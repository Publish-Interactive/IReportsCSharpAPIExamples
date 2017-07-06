using System.Collections.Generic;

public class ProductImportDataModel
{
    public object Upload { get; set; }

    /// <summary>How the product should display the import.</summary>
    public ImportTarget? Target { get; set; }
}

public class ProductUploadStatusDataModel
{
    /// <summary>The stage the job is in</summary>
    public UploadStatus? Stage { get; set; }

    /// <summary>Messages reported during processing of the document</summary>
    public List<string> Messages { get; set; }

    public ProductUploadStylesDataModel Styles { get; set; }
}

public class ProductUploadStylesDataModel
{
    public List<string> PossibleStyles { get; set; }

    /// <summary>The styles which have been mapped. Keys are the style name in the document, values should be one of the 'PossibleStyles'</summary>
    public Dictionary<string, string> MappedStyles { get; set; }

    public List<string> UnmappedStyles { get; set; }
}
public class ImportStylesForm
{
    public string Extension { get; set; }

    /// <summary>The dictionary of styles to map from and to. The properties should be simple strings, which are one of the PossibleStyles for the extension</summary>
    public object Styles { get; set; }
}

public class ImportedDocumentStylesDataModel
{
    /// <summary>The list of style names that are allowed</summary>
    public List<string> PossibleStyles { get; set; }

    /// <summary>The styles which have been mapped. Keys are the style name in the document, values should be one of the 'PossibleStyles'</summary>
    public Dictionary<string, string> MappedStyles { get; set; }
}

public class WelcomeEmailForm
{
    /// <summary>The names of accounts to send the emails for.</summary>
    public List<string> AccountNames { get; set; }
}

public class AccountCreateForm
{

    /// <summary>The name of the company the account is for.</summary>
    public string CompanyName { get; set; }

    /// <summary>The country code for the account</summary>
    public string CountryCode { get; set; }

    /// <summary>Is the account enabled.</summary>
    public bool? IsEnabled { get; set; }
}

public class AccountRegistrationCreateForm
{
    /// <summary>Registration type of account.</summary>
    public AccountRegistrationType? RegistrationType { get; set; }
}

public class AccountAccessSearchResults
{
    public string AccountName { get; set; }

    /// <summary>The registration level of the account.</summary>
    public AccountRegistrationType? RegistrationType { get; set; }

    /// <summary>The secure unique access code used to register into the account.</summary>
    public string AccessCode { get; set; }
}

public class AccountSearchResults
{
    public string Name { get; set; }

    /// <summary>The name of the company attached to account.</summary>
    public string CompanyName { get; set; }

    /// <summary>The country code attached to account.</summary>
    public string CountryCode { get; set; }

    /// <summary>Whether the account is enabled.</summary>
    public bool? IsEnabled { get; set; }
}

public class ProductSearchResults
{
    /// <summary>The products which have matched the query</summary>
    public List<ProductSummary> Products { get; set; }

    /// <summary>Total number of products which match the query, disregarding Skip and Take</summary>
    public int? ProductCount { get; set; }
}

public class ProductSummary
{
    /// <summary>Unique code of the product</summary>
    public string ProductCode { get; set; }

    /// <summary>Title of the product</summary>
    public string Title { get; set; }

    /// <summary>Url to the product in the front-end UI</summary>
    public string UILink { get; set; }

    /// <summary>Url to more information on this product in this API</summary>
    public string ApiLink { get; set; }

    /// <summary>Whether the current user has a license to this product or not.</summary>
    public bool? Licensed { get; set; }
}

public class ProductRevisionIdModel
{
    /// <summary>The code of the library the product is in</summary>
    public string LibraryCode { get; set; }

    /// <summary>Unique code of the product</summary>
    public string ProductCode { get; set; }

    /// <summary>The code identifying the revision of the product</summary>
    public string RevisionCode { get; set; }
}

public class AttachmentFileModel
{
    public string FileName { get; set; }

    public string Extension { get; set; }

    public string FileCode { get; set; }
}

public class ProductMetadataModel
{
    /// <summary>Unique code of the product</summary>
    public string ProductCode { get; set; }

    /// <summary>Title of the product. Can contain HTML</summary>
    public string Title { get; set; }

    /// <summary>Subtitle of the product. Can contain HTML</summary>
    public string Subtitle { get; set; }

    /// <summary>Description the product. Can contain HTML</summary>
    public string Description { get; set; }

    /// <summary>Price of the product</summary>
    public float? Price { get; set; }

    /// <summary>Label of the price</summary>
    public string PriceLabel { get; set; }

    /// <summary>Date and time of product publication. Null if product is continuously updated</summary>
    public System.DateTime? PublicationDate { get; set; }

    /// <summary>Shown in addition to the description on marketing pages. Can contain HTML</summary>
    public string MarketingDescription { get; set; }

    /// <summary>Whether the product is private or not</summary>
    public bool? IsPrivate { get; set; }

    /// <summary>Whether the product is coming soon or not</summary>
    public bool? IsComingSoon { get; set; }

    /// <summary>Whether the product is archived or not</summary>
    public bool? IsArchived { get; set; }

    /// <summary>Whether the product is disabled or not</summary>
    public bool? IsDisabled { get; set; }

    /// <summary>Type of the product</summary>
    public string Type { get; set; }

    /// <summary>Shown when navigating between products within the same series</summary>
    public string ShortTitle { get; set; }

    public ProductExtendedMetadataModel ExtendedMetadata { get; set; }

    public List<ProductAuthorForm> Authors { get; set; }

    public List<string> Categories { get; set; }

    public ProductBuyItNowDataModel BuyItNowLink { get; set; }

    public List<ProductLinkModel> Links { get; set; }

    public List<AttachmentFileModel> PrintCopies { get; set; }

    public List<AttachmentFileModel> Attachments { get; set; }
}

public class ProductMetadataForm
{
    /// <summary>Title of the product. Can contain HTML.
    /// 
    /// NB. Ignored for products pulled from external systems (like Dataviewer)
    /// </summary>
    public string Title { get; set; }

    /// <summary>Subtitle of the product. Must not have more than 200 characters. Can contain HTML.
    /// 
    /// NB. Ignored for products pulled from external systems (like Dataviewer)
    public string Subtitle { get; set; }

    /// <summary>Description of the product. Can contain HTML.
    /// 
    /// NB. Ignored for products pulled from external systems (like Dataviewer)
    public string Description { get; set; }

    /// <summary>Price of the product. Must be greater than or equal to zero.</summary>
    public int? Price { get; set; }

    /// <summary>Label of the price</summary>
    public string PriceLabel { get; set; }

    /// <summary>Date and time of product publication. Null if product is continuously updated
    /// 
    /// NB. Ignored for products pulled from external systems (like Dataviewer)
    /// </summary>
    public System.DateTime? PublicationDate { get; set; }

    /// <summary>Shown in addition to the description on marketing pages. Can contain HTML</summary>
    public string MarketingDescription { get; set; }

    /// <summary>Whether the product is private or not</summary>
    public bool? IsPrivate { get; set; }

    /// <summary>Whether the product is coming soon or not</summary>
    public bool? IsComingSoon { get; set; }

    /// <summary>Whether the product is archived or not</summary>
    public bool? IsArchived { get; set; }

    /// <summary>Whether the product is disabled or not</summary>
    public bool? IsDisabled { get; set; }

    /// <summary>Type of the product
    /// 
    /// NB. Ignored for products pulled from external systems (like Dataviewer)
    /// </summary>
    public ProductType Type { get; set; }

    /// <summary>Shown when navigating between products within the same series</summary>
    public string ShortTitle { get; set; }

    /// <summary>Indicates that the specified product should be created and if it already exists an error code should be returned.</summary>
    public bool? CreateOnly { get; set; }
}

public class ProductExtendedMetadataModel
{
    /// <summary>Date and time of product creation</summary>
    public System.DateTime? CreatedDate { get; set; }

    /// <summary>Markerting Url to provide users extra source of information</summary>
    public string MarketingLink { get; set; }

    /// <summary>Marketing Toc Url to an alternative Toc, perhaps containing more detail</summary>
    public string MarketingTocLink { get; set; }

    /// <summary>Language code of the product</summary>
    public string LanguageCode { get; set; }

    /// <summary>List of autosuggest keywords/phrases, comma separated</summary>
    public List<string> MetaKeywords { get; set; }

    /// <summary>The number of pages in the product. Must be non-negative, but can be null</summary>
    public int? PageCount { get; set; }

    /// <summary>Username of the last user to have updated the product</summary>
    public string LastUpdatedBy { get; set; }

    /// <summary>Date and time of when content has been imported to product</summary>
    public System.DateTime? ContentImportedOn { get; set; }

    /// <summary>Legacy field please ignore</summary>
    public string ExtraTextfield { get; set; }

    /// <summary>Date and time of when the content has been updated in product</summary>
    public System.DateTime? ContentUpdatedOn { get; set; }

    /// <summary>News chapter title of news feeds in the toc of the product.</summary>
    public string NewsTitle { get; set; }

    /// <summary>Optional metadata that can be stored with the product.
    /// 
    /// This property should be treated as a dictionary of string:string, e.g.
    /// ```
    /// {
    ///   ...
    ///   "LanguageCode": "en",
    ///   ...
    ///   "ThirdPartyMetadata": 
    ///   {
    ///     "CustomField": "CustomValue"
    ///   }
    /// }
    /// ```
    /// </summary>
    public object ThirdPartyMetadata { get; set; }
}

public class ProductExtendedMetadataForm
{
    /// <summary>Markerting Url to provide users extra source of information. If a marketing brochure file is present, this link will not be displayed</summary>
    public string MarketingLink { get; set; }

    /// <summary>Marketing Toc Url to an alternative Toc, perhaps containing more detail</summary>
    public string MarketingTocLink { get; set; }

    /// <summary>Language code of the product</summary>
    public string LanguageCode { get; set; }

    /// <summary>List of autosuggest keywords/phrases, comma separated</summary>
    public List<string> MetaKeywords { get; set; }

    /// <summary>The number of pages in the product. Must be non-negative, but can be null</summary>
    public int? PageCount { get; set; }

    /// <summary>Legacy field please ignore</summary>
    public string ExtraTextfield { get; set; }

    /// <summary>News chapter title of news feeds in the toc of the product.</summary>
    public string NewsTitle { get; set; }

    /// <summary>Optional metadata that can be stored with the product.
    /// 
    /// This property should be treated as a dictionary of string:string, e.g.
    /// ```
    /// {
    ///   ...
    ///   "LanguageCode": "en",
    ///   ...
    ///   "ThirdPartyMetadata": 
    ///   {
    ///     "CustomField": "CustomValue"
    ///   }
    /// }
    /// ```
    /// </summary>
    public object ThirdPartyMetadata { get; set; }
}

public class ProductExtendedMetadataCommand
{
    /// <summary>The operation to perform to the metadata identified with "path".
    /// 
    /// If "Add" then the metadata will be created or updated with the value specified.
    /// 
    /// If "Delete" then the metadata will be deleted, if it exists.
    /// </summary>
    public ExtendedMetadataOperation Op { get; set; }

    /// <summary>The name of the metadata to modify. Must begin with a forward slash.
    /// 
    /// See the PUT method to view the names and validation details for each metadata item.
    /// 
    /// **Paths are case sensitive**, and usually start with a capital letter (even if the json states it is lowercase)
    /// 
    /// Third Party Metadata paths should be prefixed with "/ThirdPartyMetadata/". If the custom metadata field contains a forward slash, it should be escaped by replacing it with two forward slashes.
    /// 
    /// e.g. `/ThirdPartyMetadata/Custom//Field` will reference the `Custom/Field` metadata.
    /// </summary>
    public string Path { get; set; }

    /// <summary>See the PUT method to view the valid values.</summary>
    public object Value { get; set; }
}

public class CategoryModel
{
    /// <summary>The unique identifier of the category.</summary>
    public string Id { get; set; }

    /// <summary>The id of the parent of this category. Null for a top level category.</summary>
    public string ParentId { get; set; }

    /// <summary>The optional code used to identify the category</summary>
    public string Code { get; set; }

    /// <summary>The display name of the category</summary>
    public string Name { get; set; }

    /// <summary>Whether the category is visible or not</summary>
    public bool? IsHidden { get; set; }

    /// <summary>The ids of the children belonging to this category. Replaced by Children when "Include descendants" is true</summary>
    public List<string> ChildIds { get; set; }

    /// <summary>The children of this category. Only included when "Include descendants" is true</summary>
    public List<CategoryModel> Children { get; set; }
}

public class ProductAuthorForm
{
    /// <summary>The name of the author, suitable for displaying. Must be less than 200 characters</summary>
    public string Name { get; set; }

    /// <summary>The username of the author, if the author is also a user on the system. Only available for users in role 'ReportAdmin'.</summary>
    public string Username { get; set; }
}

public class ProductTocModel
{
    public List<ProductTocEntryModel> Chapters { get; set; }

    public List<ProductTocEntryModel> Attachments { get; set; }
}

public class ProductTocEntryModel
{
    /// <summary>The summary (display name) of the entry</summary>
    public string Summary { get; set; }

    /// <summary>The contents of the current entry which are visible to the the current user.
    /// Generally, entries will recurse to 3 or 4 levels deep
    /// 
    /// Warning: This property can be null, in order to reduce the amount of data being transferred.
    /// </summary>
    public List<ProductTocEntryModel> Contents { get; set; }
}

public class NewsItemSearchResult
{
    /// <summary>The total number of items matching the search, before paging is applied.</summary>
    public int? ItemCount { get; set; }

    public List<NewsItemDataModel> Items { get; set; }
}

public class NewsItemDataModel
{
    /// <summary>The unique id of the news item</summary>
    public int? Id { get; set; }

    public string Title { get; set; }

    public string Link { get; set; }

    /// <summary>The description/content of the news item. Can contain HTML</summary>
    public string Description { get; set; }

    public System.DateTime? Date { get; set; }

    public string Author { get; set; }

    public string Source { get; set; }

    public string SourceUrl { get; set; }

    public string Guid { get; set; }

    public string EnclosureUrl { get; set; }

    public int? EnclosureLength { get; set; }

    public string EnclosureType { get; set; }

    public string CommentsUrl { get; set; }

    public List<RssCategoryDataModel> Categories { get; set; }

    public List<NewsItemReferenceDataModel> References { get; set; }

    /// <summary>News items can optionally be marked as 'featured', and can be displayed seperately in news widgets or emails.</summary>
    public bool? IsFeatured { get; set; }
}

public class RssCategoryDataModel
{
    public string CategoryPath { get; set; }

    public string Domain { get; set; }
}

public class NewsItemReferenceDataModel
{
    public string Name { get; set; }

    /// <summary>Must be a valid, absolute URI</summary>
    public string Url { get; set; }
}

public class SubscriberDataModel
{
    public string Username { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    public string AccountName { get; set; }

    public bool? IsEnabled { get; set; }

    public string Company { get; set; }

    public string CountryCode { get; set; }

    public string LanguageCode { get; set; }
}

public class SubscriberDataForm
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    /// <summary>Moves user to specified account. If null then user stays in current location. If user is moved then all 
    /// comments made by that user are deleted. The user will lose access to licenses they inherited from the 
    /// old account.
    /// </summary>
    public string AccountName { get; set; }

    public bool? IsEnabled { get; set; }

    public string Company { get; set; }

    public string CountryCode { get; set; }

    public string LanguageCode { get; set; }

    /// <summary>For newly created subscribers specifies whether to send them a welcome email containing a temporary password.
    /// Must be false for existing subscribers.
    /// </summary>
    public bool? SendWelcomeEmail { get; set; }
}

public class CreateUserLicenseForm
{
    public string Report { get; set; }

    public LicenseDateRangeForm ActiveDates { get; set; }

    public bool? AllowAllAttachmentExtensions { get; set; }

    /// <summary>A list of space separated attachment extensions (not starting with a '.') that are allowed to be downloaded. If AllowAllAttachmentExtensions is true then it should be null, if AllowAllAttachmentExtensions is false then it should not be null
    /// </summary>
    public List<string> AllowedAttachmentExtensions { get; set; }

    /// <summary>If not set no news subscriptions will be added.</summary>
    public NewsSubscriptions? NewsSubscriptions { get; set; }

    public bool? SendConfirmationEmail { get; set; }

    /// <summary>When true will add a license to all underlying data products of embedded exhibits, allowing end users to interact with them.</summary>
    public bool? LicenseDataProducts { get; set; }
}

public class LicenseDateRangeForm
{
    public System.DateTime? StartDate { get; set; }

    /// <summary>The license will be active up to this date-time.</summary>
    public System.DateTime? EndDate { get; set; }
}

public class ExternalProductLinkForm
{
    /// <summary>The URL displayed to the user.</summary>
    public string DisplayUrl { get; set; }

    /// <summary>The actual URL used (should start with either http:// or https://).</summary>
    public string LoginUrl { get; set; }

    /// <summary>If set to false the request type will be HTTP GET and the parameters will form the query string. If set to true the request type will be HTTP POST and the paramters will form the request body.    
    /// </summary>
    public bool? LoginIsPost { get; set; }

    /// <summary>If set to 'AutoHideTabs' the link will automatically open in an iframe in the page (with report tabs hidden) as soon as the report is accessed. If set to 'AutoShowTabs' the link will open automatically, when the report is accessed, in an iframe in the page. If set to 'ManualPopup' when the access button is clicked the link will open in an iframe in lightbox. If set to 'ManualNewTab' when the access button is clicked the link will open in a new tab.      
    /// </summary>
    public AutoSubmitMode? AutoSubmitMode { get; set; }

    public List<ExternalProductLinkParameterModel> Parameters { get; set; }
}

public class ExternalProductLinkParameterModel
{
    /// <summary>Name of the parameter.</summary>
    public string Name { get; set; }

    /// <summary>The value of the parameter.</summary>
    public string Value { get; set; }

    /// <summary>Whether to display the parameter and its value to the user. It will always be sent when they access the product regardless of this setting.</summary>
    public bool? IsVisible { get; set; }
}

public class CreateCategoryLicenseForm
{
    /// <summary>The category to add the license to (use '-' to separate category and subcategory names e.g. category-subcategory).</summary>
    public string Category { get; set; }

    public LicenseDateRangeForm ActiveDates { get; set; }

    /// <summary>If set, the user will only gain access to reports with publication dates within this range.</summary>
    public LicenseDateRangeForm PublicationDates { get; set; }

    /// <summary>The number of days after a reports publication date that the user will gain access.</summary>
    public int? PublicationEmbargo { get; set; }

    /// <summary>A list of space-separated attachment extensions (not starting with a '.') that are allowed to be downloaded. If AllowAllAttachmentExtensions is true then it should be null, if AllowAllAttachmentExtensions is false then it should not be null.
    /// </summary>
    public List<string> AllowedAttachmentExtensions { get; set; }

    /// <summary>Whether to allow all attachments to be downloaded.</summary>
    public bool? AllowAllAttachmentExtensions { get; set; }

    /// <summary>If not set no news subscriptions will be added.</summary>
    public NewsSubscriptions? NewsSubscriptions { get; set; }
}

public class ProductBuyItNowDataModel
{
    public string BuyItNowLink { get; set; }

    public bool? IsEnabled { get; set; }
}

public class ProductLinkModel
{
    public int? Id { get; set; }

    public string Name { get; set; }
}

public class ProductLinkDetailsModel
{
    public int? Id { get; set; }

    public string Name { get; set; }

    public ProductLinkType? Type { get; set; }

    public List<string> LinkedReportCodes { get; set; }
}

public class ProductLinkDetailsForm
{
    public string Name { get; set; }

    public ProductLinkType Type { get; set; }

    public List<string> LinkedReportCodes { get; set; }
}

public class FileDownloadModel
{
    public string Name { get; set; }

    public int? FileSize { get; set; }

    /// <summary>In JSON format this will be a base 64 encoded string but we recommend using BSON format for this request.</summary>
    public byte[] Content { get; set; }
}

public class ProductContentModel
{
    public string Type { get; set; }

    public FileDownloadModel Content { get; set; }
}

public class MarketingBrochureForm
{
    public List<object> Content { get; set; }

    /// <summary>The display name (including the extension) of the file to upload</summary>
    public string DisplayName { get; set; }
}

public class MarketingBrochureModel
{
    public string Url { get; set; }

    /// <summary>The display name of the brochure</summary>
    public string DisplayName { get; set; }

    /// <summary>The size of the brochure in bytes</summary>
    public string Size { get; set; }
}

public class CategoryDataForm
{
    public string Name { get; set; }

    /// <summary>The id of the parent (if not set then it will be a top level category)</summary>
    public string ParentId { get; set; }

    /// <summary>Whether the category is only visible to report admins</summary>
    public bool? IsHidden { get; set; }

    /// <summary>The optional code used to identify the category (must be unique)</summary>
    public string Code { get; set; }
}

public class ProductSearchParametersForm
{
    public string Terms { get; set; }

    /// <summary>When provided, filters the returned products to only include the products in the categories by category path. When two categories are under the same top-level category, products in either category are returned ("OR"). When two categories are in different top-level categories, only products which are in both categories are returned ("AND")</summary>
    public List<string> CategoryPaths { get; set; }

    /// <summary>Specifies the order in which the products should be returned, as a comma-seperated string of fields. The field can be 'Title', 'Published', followed by '.asc', '.desc'. These options can be changed by contacting support.</summary>
    public string OrderBy { get; set; }

    /// <summary>Whether to include products which are not licensed to the current user</summary>
    public bool? IncludeUnlicensed { get; set; }

    /// <summary>Whether to include private products in the results, which the current user has a license to</summary>
    public bool? IncludePrivate { get; set; }

    /// <summary>Whether to include coming soon products in the results</summary>
    public bool? IncludeComingSoon { get; set; }

    /// <summary>Whether to include archived products in the results</summary>
    public bool? IncludeArchived { get; set; }

    /// <summary>When set, results only include reports which are not older than the timespan indicated.
    /// Report age is determined from the published date.
    /// By default the only values that are allowed are ANY, 3Y, 2Y, 1Y or 6M but this can be configured by contacting support.
    /// </summary>
    public string MaxAge { get; set; }

    /// <summary>When set, filters results to only include products of the specified type. When not set, no filtering is applied.</summary>
    public List<SearchProductType> ProductTypes { get; set; }
}

public class AddSavedSearchForm
{
    public string Title { get; set; }

    public bool? IsPinnedToHomepage { get; set; }

    public bool? IsShared { get; set; }

    public AlertFrequency? AlertFrequency { get; set; }

    public ProductSearchParametersForm SearchParameters { get; set; }
}

public class SavedSearchListModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public bool? IsPinnedToHomepage { get; set; }

    public bool? IsShared { get; set; }

    public AlertFrequency? AlertFrequency { get; set; }
}

public class SavedSearchModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public bool? IsPinnedToHomepage { get; set; }

    public bool? IsShared { get; set; }

    public AlertFrequency? AlertFrequency { get; set; }

    public ProductSearchParametersForm SearchParameters { get; set; }
}

public class CreatedCategoryModel
{
    public string Id { get; set; }

    /// <summary>The id of the parent of this category. Null for a top level category.</summary>
    public string ParentId { get; set; }

    /// <summary>The optional code used to identify the category</summary>
    public string Code { get; set; }

    /// <summary>The display name of the category</summary>
    public string Name { get; set; }

    /// <summary>Whether the category is visible or not</summary>
    public bool? IsHidden { get; set; }
}

public class LicenseModel
{
    public int? Id { get; set; }

    /// <summary>The category id (uses '-' to separate category and subcategory names e.g. category-subcategory).</summary>
    public string Category { get; set; }

    /// <summary>The report code the license is for.</summary>
    public string Report { get; set; }

    public LicenseDateRangeForm ActiveDates { get; set; }

    /// <summary>If set, the user will only gain access to reports with publication dates within this range.</summary>
    public LicenseDateRangeForm PublicationDates { get; set; }

    /// <summary>The number of days after a reports publication date that the user will gain access.</summary>
    public int? PublicationEmbargo { get; set; }

    /// <summary>A list of space-separated attachment extensions (* if all attachments are allowed).</summary>
    public string AllowedAttachments { get; set; }

    /// <summary>If not set no news subscriptions is set.</summary>
    public NewsSubscriptions? NewsSubscriptions { get; set; }

    /// <summary>The username of the user the license is for (if not set then the license is for an account).</summary>
    public string Username { get; set; }

    /// <summary>The account name of the account the license is for.</summary>
    public string AccountName { get; set; }

    /// <summary>Whether the license has been deleted.</summary>
    public bool? IsDisabled { get; set; }
}

public class LicenseQueryResultModel<T>
{
    public List<T> Licenses { get; set; }

    public int TotalCount { get; set; }
}

public class ReportLicenseModel
{
    public int Id { get; set; }

    public string Report { get; set; }

    public LicenseDateRangeForm ActiveDates { get; set; }

    public string AllowedAttachments { get; set; }

    public NewsSubscriptions? NewsSubscriptions { get; set; }

    public string Username { get; set; }

    public string AccountName { get; set; }
}

public class CategoryLicenseModel
{
    public int Id { get; set; }

    public string Category { get; set; }

    public LicenseDateRangeForm ActiveDates { get; set; }

    public LicenseDateRangeForm PublicationDates { get; set; }

    public int? PublicationEmbargo { get; set; }

    public string AllowedAttachments { get; set; }

    public NewsSubscriptions? NewsSubscriptions { get; set; }

    public string Username { get; set; }

    public string AccountName { get; set; }
}

/// <summary>The direction to sort news items by published date, and then id.</summary>
public enum SortDirection
{
    Ascending,
    Descending,
}

public enum ImportTarget
{
    MainDeliverable,
    Attachment,
    PrintCopy,
}

public enum UploadStatus
{
    Failed,
    NeedsStyles,
    Processing,
    Complete,
}

public enum AccountRegistrationType
{
    NoRegistration,
    AccessCodeOnly,
    AllowAnyone,
    GuestUserButNoRegistration,
}

public enum ProductType
{
    Normal,
    PDF,
    External,
    PowerPoint,
    Excel,
    Word,
    NewsFeed,
    Database,
}

public enum ExtendedMetadataOperation
{
    Add,
    Delete,
}

public enum NewsSubscriptions
{
    Manual,
    Automatic,
}

public enum AutoSubmitMode
{
    AutoHideTabs,
    AutoShowTabs,
    ManualPopup,
    ManualNewTab,
}

public enum ProductLinkType
{
    Translation,
    External,
    Grouping,
    General,
    Series,
}

public enum SearchProductType
{
    Interactive,
    Pdf,
    External,
    DataProduct,
    PowerPoint,
    Excel,
    Word,
    NewsFeed,
}

public enum AlertFrequency
{
    Never,
    Daily,
    Weekly,
}
