using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IReportsApiExamples.Examples
{
    public class ImportCategoriesAndProducts
    {
        /// <summary>Reads the categories and products from specified Json files and uploads them to the server</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="categoriesFilePath">The path to the categories Json file</param>
        /// <param name="categoriesFilePath">The path to the products Json file</param>
        /// <param name="libraryCode">The code of the desired library to put the categories and products in</param>
        public static async Task DoWork(
            ApiWrapper wrapper,
            string categoriesFilePath,
            string productsFilePath,
            string libraryCode)
        {
            var categoryList = GetListFromJson<CategoryModel>(categoriesFilePath);
            var productList = GetListFromJson<ProductMetadataModel>(productsFilePath);

            await PostCategories(wrapper, categoryList);
            await PutProducts(wrapper, productList, FlattenCategoriesAndSetParentId(GetListFromJson<CategoryModel>(categoriesFilePath)).ToList(), libraryCode);
        }

        /// <summary>Put the products first. Then put the extended metadata, authors and the list
        /// of categories the product is in, if the product model includes them</summary>

        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="productList">The list of deserialized products from the Json file</param>
        /// <param name="libraryCode">The code of the desired library to put the products in</param>
        private static async Task PutProducts(
            ApiWrapper wrapper,
            List<ProductMetadataModel> productList,
            List<CategoryModel> categoryList,
            string libraryCode)
        {
            foreach (var product in productList)
            {
                await wrapper.PutProductAsync(
                    libraryCode,
                    product.ProductCode,
                    CreateProductMetadataForm(product));

                if (product.ExtendedMetadata != null)
                {
                    await wrapper.PutExtendedMetadataAsync(
                        libraryCode,
                        product.ProductCode,
                        CreateProductExtendedMetadataForm(product.ExtendedMetadata));
                }

                if (product.Authors.Count > 0)
                {
                    await wrapper.PutProductAuthorsAsync(
                        libraryCode,
                        product.ProductCode,
                        product.Authors);
                }

                if (product.Categories.Count > 0)
                {
                    await wrapper.PutProductCategoriesAsync(
                        libraryCode,
                        product.ProductCode,
                        GenerateProductCategoryIds(product.Categories, categoryList).ToList()
                    );
                }

                Console.WriteLine($"Created product {product.Title}");
            }
        }

        /// <summary>Joins the parentId of each category in the list of product categories because on the server,
        /// the categoryIds are joined with their parentIds<summary>

        /// <param name="productCategories">The list of categories to concatinate with their parentIds<param>
        /// <param name="categoryList">The list of CategoryModels to search through to find the parentIds 
        /// of the product categories<param>
        private static IEnumerable<string> GenerateProductCategoryIds(
            List<string> productCategories,
            List<CategoryModel> categoryList)
        {
            foreach (var productCategoryId in productCategories)
            {
                var parentId = categoryList.Where(c => c.Id == productCategoryId).Single().ParentId;

                if (parentId != null)
                {
                    yield return string.Join("-", parentId, productCategoryId);
                }
                else
                {
                    yield return productCategoryId;

                }
            }
        }

        /// <summary>Posts the categories and checks whether they already exist</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="categoryList">The list of deserialized categories from the Json file</param>
        private static async Task PostCategories(ApiWrapper wrapper, List<CategoryModel> categoryList)
        {
            var categoryIds = await GetCategoryIds(wrapper);

            foreach (var category in FlattenCategoriesAndSetParentId(categoryList).Reverse())
            {
                var oldId = category.Id;

                if (category.ParentId != null)
                {
                    category.Id = $"{category.ParentId}-{category.Id}";
                }

                if (categoryIds.Contains(category.Id))
                {
                    Console.WriteLine($"Category '{category.Name}' already exists");
                    continue;
                }
                else
                {
                    category.Id = oldId;
                    await wrapper.PostCategoryAsync(CreateCategoryDataForm(category));
                    Console.WriteLine($"Created Category '{category.Name}'");
                }
            }
        }

        /// <summary>Gets the codes of all the categories that already exist on the server</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        private static async Task<List<string>> GetCategoryIds(ApiWrapper wrapper)
        {
            var categoryList = await wrapper.GetCategoriesAsync(true, true);

            return FlattenCategories(categoryList).Select(c => c.Id).ToList();
        }

        /// <summary>Traverses through the category tree and flattens it so all the items are in one list</summary>
        /// <param name="categoryList">The list of categories to flatten</param>
        /// <param name="id">(ignore unless recursing) The Id of the parent category, used to generate the parentId
        /// of each category in the list</param>
        private static IEnumerable<CategoryModel> FlattenCategoriesAndSetParentId(
            List<CategoryModel> categoryList,
            string id = null)
        {
            foreach (var category in categoryList)
            {
                if (id != null)
                {
                    category.ParentId = id.TrimEnd('-');
                }
                var newId = id + category.Id + "-";
                if (category.Children != null)
                {
                    foreach (var childCategory in FlattenCategoriesAndSetParentId(category.Children, newId))
                    {
                        yield return childCategory;
                    }
                }

                yield return category;
            }
        }

        /// <summary>Traverses through the category tree and flattens it so all the items are in one list</summary>
        /// <param name="categoryList">The list of categories to flatten</param>
        private static IEnumerable<CategoryModel> FlattenCategories(List<CategoryModel> categoryList)
        {
            foreach (var category in categoryList)
            {
                if (category.Children != null)
                {
                    foreach (var childCategory in FlattenCategories(category.Children))
                    {
                        yield return childCategory;
                    }
                }

                yield return category;
            }
        }

        /// <summary>Mirrors the data in the ExtendedMetadataModel to the ExtendedMetadataForm so the Form can be
        /// put on the server</summary>

        /// <param name="extendedMetadataModel">The ProductExtendedMetadataModel to convert to a Form</param>
        private static ProductExtendedMetadataForm CreateProductExtendedMetadataForm(
            ProductExtendedMetadataModel extendedMetadataModel)
        {
            return new ProductExtendedMetadataForm
            {
                MarketingLink = extendedMetadataModel.MarketingLink,
                MarketingTocLink = extendedMetadataModel.MarketingTocLink,
                LanguageCode = extendedMetadataModel.LanguageCode,
                MetaKeywords = extendedMetadataModel.MetaKeywords,
                PageCount = extendedMetadataModel.PageCount,
                ExtraTextfield = extendedMetadataModel.ExtraTextfield,
                NewsTitle = extendedMetadataModel.NewsTitle,
                ThirdPartyMetadata = extendedMetadataModel.ThirdPartyMetadata
            };
        }

        /// <summary>Mirrors the data in the MetadataModel to the MetadataForm so the Form can be
        /// put on the server</summary>

        /// <param name="metadataModel">The ProductMetadataModel to convert to a Form</param>
        private static ProductMetadataForm CreateProductMetadataForm(ProductMetadataModel metadataModel)
        {
            return new ProductMetadataForm
            {
                Title = metadataModel.Title,
                Subtitle = metadataModel.Subtitle,
                Description = metadataModel.Description,
                Price = (int)metadataModel.Price,
                PriceLabel = metadataModel.PriceLabel,
                PublicationDate = metadataModel.PublicationDate,
                MarketingDescription = metadataModel.MarketingDescription,
                IsPrivate = metadataModel.IsPrivate,
                IsComingSoon = metadataModel.IsComingSoon,
                IsArchived = metadataModel.IsArchived,
                IsDisabled = metadataModel.IsDisabled,
                Type = GetProductType(metadataModel.Type),
                ShortTitle = metadataModel.ShortTitle,
                CreateOnly = false
            };
        }

        /// <summary>Mirrors the data in the CategoryModel to the CategoryDataForm so the Form can be
        /// uploaded to the server</summary>

        /// <param name="categoryModel">The CategoryModel to convert to a Form</param>
        private static CategoryDataForm CreateCategoryDataForm(CategoryModel categoryModel)
        {
            return new CategoryDataForm
            {
                Name = categoryModel.Name,
                ParentId = categoryModel.ParentId,
                Code = categoryModel.Code,
                IsHidden = categoryModel.IsHidden
            };
        }

        /// <summary>Converts a product type string to an Enum</summary>
        /// <param name="typeString">The product type in the form of a string</param>
        private static ProductType GetProductType(string typeString)
        {
            return (ProductType)Enum.Parse(typeof(ProductType), typeString);
        }
        /// <summary>Deserializes the desired Json file into a list with a specified type</summary>
        /// <param name="filePath">The path to the Json file to be deserialized</param>
        private static List<T> GetListFromJson<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
        }
    }
}