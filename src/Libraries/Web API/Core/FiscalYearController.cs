using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.Api.Framework;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Newtonsoft.Json;
using PetaPoco;

namespace MixERP.Net.Api.Core
{
    /// <summary>
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Fiscal Years.
    /// </summary>
    [RoutePrefix("api/v1.5/core/fiscal-year")]
    public class FiscalYearController : ApiController
    {
        /// <summary>
        ///     The FiscalYear data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Core.Data.FiscalYear FiscalYearContext;

        public FiscalYearController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.FiscalYearContext = new MixERP.Net.Schemas.Core.Data.FiscalYear
            {
                Catalog = this.Catalog,
                LoginId = this.LoginId,
                UserId = this.UserId
            };
        }

        public long LoginId { get; }
        public int UserId { get; private set; }
        public int OfficeId { get; private set; }
        public string Catalog { get; }

        /// <summary>
        ///     Creates meta information of "fiscal year" entity.
        /// </summary>
        /// <returns>Returns the "fiscal year" meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("meta")]
        [Route("~/api/core/fiscal-year/meta")]
        public EntityView GetEntityView()
        {
            return new EntityView
            {
                PrimaryKey = "fiscal_year_code",
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "fiscal_year_code",  PropertyName = "FiscalYearCode",  DataType = "string",  DbDataType = "varchar",  IsNullable = false,  IsPrimaryKey = true,  IsSerial = false,  Value = "",  MaxLength = 12 },
                                        new EntityColumn { ColumnName = "fiscal_year_name",  PropertyName = "FiscalYearName",  DataType = "string",  DbDataType = "varchar",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 50 },
                                        new EntityColumn { ColumnName = "starts_from",  PropertyName = "StartsFrom",  DataType = "DateTime",  DbDataType = "date",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "ends_on",  PropertyName = "EndsOn",  DataType = "DateTime",  DbDataType = "date",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "audit_user_id",  PropertyName = "AuditUserId",  DataType = "int",  DbDataType = "int4",  IsNullable = true,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "audit_ts",  PropertyName = "AuditTs",  DataType = "DateTime",  DbDataType = "timestamptz",  IsNullable = true,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }

        /// <summary>
        ///     Counts the number of fiscal years.
        /// </summary>
        /// <returns>Returns the count of the fiscal years.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/core/fiscal-year/count")]
        public long Count()
        {
            try
            {
                return this.FiscalYearContext.Count();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Returns collection of fiscal year for export.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("export")]
        [Route("~/api/core/fiscal-year/export")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> Get()
        {
            try
            {
                return this.FiscalYearContext.Get();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Returns an instance of fiscal year.
        /// </summary>
        /// <param name="fiscalYearCode">Enter FiscalYearCode to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{fiscalYearCode}")]
        [Route("~/api/core/fiscal-year/{fiscalYearCode}")]
        public MixERP.Net.Entities.Core.FiscalYear Get(string fiscalYearCode)
        {
            try
            {
                return this.FiscalYearContext.Get(fiscalYearCode);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "HEAD")]
        [Route("get")]
        [Route("~/api/core/fiscal-year/get")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> Get([FromUri] string[] fiscalYearCodes)
        {
            try
            {
                return this.FiscalYearContext.Get(fiscalYearCodes);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 fiscal years on each page, sorted by the property FiscalYearCode.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/core/fiscal-year")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> GetPagedResult()
        {
            try
            {
                return this.FiscalYearContext.GetPagedResult();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a paginated collection containing 25 fiscal years on each page, sorted by the property FiscalYearCode.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/core/fiscal-year/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.FiscalYearContext.GetPagedResult(pageNumber);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Counts the number of fiscal years using the supplied filter(s).
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the count of filtered fiscal years.</returns>
        [AcceptVerbs("POST")]
        [Route("count-where")]
        [Route("~/api/core/fiscal-year/count-where")]
        public long CountWhere([FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.FiscalYearContext.CountWhere(f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 fiscal years on each page, sorted by the property FiscalYearCode.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/core/fiscal-year/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.FiscalYearContext.GetWhere(pageNumber, f);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Counts the number of fiscal years using the supplied filter name.
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns the count of filtered fiscal years.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count-filtered/{filterName}")]
        [Route("~/api/core/fiscal-year/count-filtered/{filterName}")]
        public long CountFiltered(string filterName)
        {
            try
            {
                return this.FiscalYearContext.CountFiltered(filterName);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Creates a filtered and paginated collection containing 25 fiscal years on each page, sorted by the property FiscalYearCode.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("get-filtered/{pageNumber}/{filterName}")]
        [Route("~/api/core/fiscal-year/get-filtered/{pageNumber}/{filterName}")]
        public IEnumerable<MixERP.Net.Entities.Core.FiscalYear> GetFiltered(long pageNumber, string filterName)
        {
            try
            {
                return this.FiscalYearContext.GetFiltered(pageNumber, filterName);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Displayfield is a lightweight key/value collection of fiscal years.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of fiscal years.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/core/fiscal-year/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.FiscalYearContext.GetDisplayFields();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for fiscal years.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of fiscal years.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/fiscal-year/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.FiscalYearContext.GetCustomFields(null);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     A custom field is a user defined field for fiscal years.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of fiscal years.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields/{resourceId}")]
        [Route("~/api/core/fiscal-year/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.FiscalYearContext.GetCustomFields(resourceId);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds or edits your instance of FiscalYear class.
        /// </summary>
        /// <param name="fiscalYear">Your instance of fiscal years class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/core/fiscal-year/add-or-edit")]
        public void AddOrEdit([FromBody]Newtonsoft.Json.Linq.JArray form)
        {
            MixERP.Net.Entities.Core.FiscalYear fiscalYear = form[0].ToObject<MixERP.Net.Entities.Core.FiscalYear>(JsonHelper.GetJsonSerializer());
            List<EntityParser.CustomField> customFields = form[1].ToObject<List<EntityParser.CustomField>>(JsonHelper.GetJsonSerializer());

            if (fiscalYear == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.FiscalYearContext.AddOrEdit(fiscalYear, customFields);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Adds your instance of FiscalYear class.
        /// </summary>
        /// <param name="fiscalYear">Your instance of fiscal years class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{fiscalYear}")]
        [Route("~/api/core/fiscal-year/add/{fiscalYear}")]
        public void Add(MixERP.Net.Entities.Core.FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.FiscalYearContext.Add(fiscalYear);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Edits existing record with your instance of FiscalYear class.
        /// </summary>
        /// <param name="fiscalYear">Your instance of FiscalYear class to edit.</param>
        /// <param name="fiscalYearCode">Enter the value for FiscalYearCode in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{fiscalYearCode}")]
        [Route("~/api/core/fiscal-year/edit/{fiscalYearCode}")]
        public void Edit(string fiscalYearCode, [FromBody] MixERP.Net.Entities.Core.FiscalYear fiscalYear)
        {
            if (fiscalYear == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.FiscalYearContext.Update(fiscalYear, fiscalYearCode);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        private List<MixERP.Net.Entities.Core.FiscalYear> ParseCollection(dynamic collection)
        {
            return JsonConvert.DeserializeObject<List<MixERP.Net.Entities.Core.FiscalYear>>(collection.ToString(), JsonHelper.GetJsonSerializerSettings());
        }

        /// <summary>
        ///     Adds or edits multiple instances of FiscalYear class.
        /// </summary>
        /// <param name="collection">Your collection of FiscalYear class to bulk import.</param>
        /// <returns>Returns list of imported fiscalYearCodes.</returns>
        /// <exception cref="MixERPException">Thrown when your any FiscalYear class in the collection is invalid or malformed.</exception>
        [AcceptVerbs("PUT")]
        [Route("bulk-import")]
        [Route("~/api/core/fiscal-year/bulk-import")]
        public List<object> BulkImport([FromBody]dynamic collection)
        {
            List<MixERP.Net.Entities.Core.FiscalYear> fiscalYearCollection = this.ParseCollection(collection);

            if (fiscalYearCollection == null || fiscalYearCollection.Count.Equals(0))
            {
                return null;
            }

            try
            {
                return this.FiscalYearContext.BulkImport(fiscalYearCollection);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (MixERPException)
            {
                throw;
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        /// <summary>
        ///     Deletes an existing instance of FiscalYear class via FiscalYearCode.
        /// </summary>
        /// <param name="fiscalYearCode">Enter the value for FiscalYearCode in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{fiscalYearCode}")]
        [Route("~/api/core/fiscal-year/delete/{fiscalYearCode}")]
        public void Delete(string fiscalYearCode)
        {
            try
            {
                this.FiscalYearContext.Delete(fiscalYearCode);
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


    }
}