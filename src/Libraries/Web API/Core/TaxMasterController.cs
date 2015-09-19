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
    ///     Provides a direct HTTP access to perform various tasks such as adding, editing, and removing Tax Masters.
    /// </summary>
    [RoutePrefix("api/v1.5/core/tax-master")]
    public class TaxMasterController : ApiController
    {
        /// <summary>
        ///     The TaxMaster data context.
        /// </summary>
        private readonly MixERP.Net.Schemas.Core.Data.TaxMaster TaxMasterContext;

        public TaxMasterController()
        {
            this.LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this.UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this.OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this.Catalog = AppUsers.GetCurrentUserDB();

            this.TaxMasterContext = new MixERP.Net.Schemas.Core.Data.TaxMaster
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
        ///     Creates meta information of "tax master" entity.
        /// </summary>
        /// <returns>Returns the "tax master" meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("meta")]
        [Route("~/api/core/tax-master/meta")]
        public EntityView GetEntityView()
        {
            return new EntityView
            {
                PrimaryKey = "tax_master_id",
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "tax_master_id",  PropertyName = "TaxMasterId",  DataType = "int",  DbDataType = "int4",  IsNullable = false,  IsPrimaryKey = true,  IsSerial = true,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "tax_master_code",  PropertyName = "TaxMasterCode",  DataType = "string",  DbDataType = "varchar",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 12 },
                                        new EntityColumn { ColumnName = "tax_master_name",  PropertyName = "TaxMasterName",  DataType = "string",  DbDataType = "varchar",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 50 },
                                        new EntityColumn { ColumnName = "audit_user_id",  PropertyName = "AuditUserId",  DataType = "int",  DbDataType = "int4",  IsNullable = true,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "audit_ts",  PropertyName = "AuditTs",  DataType = "DateTime",  DbDataType = "timestamptz",  IsNullable = true,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }

        /// <summary>
        ///     Counts the number of tax masters.
        /// </summary>
        /// <returns>Returns the count of the tax masters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count")]
        [Route("~/api/core/tax-master/count")]
        public long Count()
        {
            try
            {
                return this.TaxMasterContext.Count();
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
        ///     Returns collection of tax master for export.
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("export")]
        [Route("~/api/core/tax-master/export")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> Get()
        {
            try
            {
                return this.TaxMasterContext.Get();
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
        ///     Returns an instance of tax master.
        /// </summary>
        /// <param name="taxMasterId">Enter TaxMasterId to search for.</param>
        /// <returns></returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("{taxMasterId}")]
        [Route("~/api/core/tax-master/{taxMasterId}")]
        public MixERP.Net.Entities.Core.TaxMaster Get(int taxMasterId)
        {
            try
            {
                return this.TaxMasterContext.Get(taxMasterId);
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
        [Route("~/api/core/tax-master/get")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> Get([FromUri] int[] taxMasterIds)
        {
            try
            {
                return this.TaxMasterContext.Get(taxMasterIds);
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
        ///     Creates a paginated collection containing 25 tax masters on each page, sorted by the property TaxMasterId.
        /// </summary>
        /// <returns>Returns the first page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("")]
        [Route("~/api/core/tax-master")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> GetPagedResult()
        {
            try
            {
                return this.TaxMasterContext.GetPagedResult();
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
        ///     Creates a paginated collection containing 25 tax masters on each page, sorted by the property TaxMasterId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <returns>Returns the requested page from the collection.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("page/{pageNumber}")]
        [Route("~/api/core/tax-master/page/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> GetPagedResult(long pageNumber)
        {
            try
            {
                return this.TaxMasterContext.GetPagedResult(pageNumber);
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
        ///     Counts the number of tax masters using the supplied filter(s).
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the count of filtered tax masters.</returns>
        [AcceptVerbs("POST")]
        [Route("count-where")]
        [Route("~/api/core/tax-master/count-where")]
        public long CountWhere([FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.TaxMasterContext.CountWhere(f);
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
        ///     Creates a filtered and paginated collection containing 25 tax masters on each page, sorted by the property TaxMasterId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("POST")]
        [Route("get-where/{pageNumber}")]
        [Route("~/api/core/tax-master/get-where/{pageNumber}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> GetWhere(long pageNumber, [FromBody]dynamic filters)
        {
            try
            {
                List<EntityParser.Filter> f = JsonConvert.DeserializeObject<List<EntityParser.Filter>>(filters);
                return this.TaxMasterContext.GetWhere(pageNumber, f);
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
        ///     Counts the number of tax masters using the supplied filter name.
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns the count of filtered tax masters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("count-filtered/{filterName}")]
        [Route("~/api/core/tax-master/count-filtered/{filterName}")]
        public long CountFiltered(string filterName)
        {
            try
            {
                return this.TaxMasterContext.CountFiltered(filterName);
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
        ///     Creates a filtered and paginated collection containing 25 tax masters on each page, sorted by the property TaxMasterId.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the resultset.</param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns the requested page from the collection using the supplied filters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("get-filtered/{pageNumber}/{filterName}")]
        [Route("~/api/core/tax-master/get-filtered/{pageNumber}/{filterName}")]
        public IEnumerable<MixERP.Net.Entities.Core.TaxMaster> GetFiltered(long pageNumber, string filterName)
        {
            try
            {
                return this.TaxMasterContext.GetFiltered(pageNumber, filterName);
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
        ///     Displayfield is a lightweight key/value collection of tax masters.
        /// </summary>
        /// <returns>Returns an enumerable key/value collection of tax masters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("display-fields")]
        [Route("~/api/core/tax-master/display-fields")]
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            try
            {
                return this.TaxMasterContext.GetDisplayFields();
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
        ///     A custom field is a user defined field for tax masters.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of tax masters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields")]
        [Route("~/api/core/tax-master/custom-fields")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields()
        {
            try
            {
                return this.TaxMasterContext.GetCustomFields(null);
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
        ///     A custom field is a user defined field for tax masters.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection of tax masters.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("custom-fields/{resourceId}")]
        [Route("~/api/core/tax-master/custom-fields/{resourceId}")]
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            try
            {
                return this.TaxMasterContext.GetCustomFields(resourceId);
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
        ///     Adds or edits your instance of TaxMaster class.
        /// </summary>
        /// <param name="taxMaster">Your instance of tax masters class to add or edit.</param>
        [AcceptVerbs("PUT")]
        [Route("add-or-edit")]
        [Route("~/api/core/tax-master/add-or-edit")]
        public void AddOrEdit([FromBody]Newtonsoft.Json.Linq.JArray form)
        {
            MixERP.Net.Entities.Core.TaxMaster taxMaster = form[0].ToObject<MixERP.Net.Entities.Core.TaxMaster>(JsonHelper.GetJsonSerializer());
            List<EntityParser.CustomField> customFields = form[1].ToObject<List<EntityParser.CustomField>>(JsonHelper.GetJsonSerializer());

            if (taxMaster == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.TaxMasterContext.AddOrEdit(taxMaster, customFields);
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
        ///     Adds your instance of TaxMaster class.
        /// </summary>
        /// <param name="taxMaster">Your instance of tax masters class to add.</param>
        [AcceptVerbs("POST")]
        [Route("add/{taxMaster}")]
        [Route("~/api/core/tax-master/add/{taxMaster}")]
        public void Add(MixERP.Net.Entities.Core.TaxMaster taxMaster)
        {
            if (taxMaster == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.TaxMasterContext.Add(taxMaster);
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
        ///     Edits existing record with your instance of TaxMaster class.
        /// </summary>
        /// <param name="taxMaster">Your instance of TaxMaster class to edit.</param>
        /// <param name="taxMasterId">Enter the value for TaxMasterId in order to find and edit the existing record.</param>
        [AcceptVerbs("PUT")]
        [Route("edit/{taxMasterId}")]
        [Route("~/api/core/tax-master/edit/{taxMasterId}")]
        public void Edit(int taxMasterId, [FromBody] MixERP.Net.Entities.Core.TaxMaster taxMaster)
        {
            if (taxMaster == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            try
            {
                this.TaxMasterContext.Update(taxMaster, taxMasterId);
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

        private List<MixERP.Net.Entities.Core.TaxMaster> ParseCollection(dynamic collection)
        {
            return JsonConvert.DeserializeObject<List<MixERP.Net.Entities.Core.TaxMaster>>(collection.ToString(), JsonHelper.GetJsonSerializerSettings());
        }

        /// <summary>
        ///     Adds or edits multiple instances of TaxMaster class.
        /// </summary>
        /// <param name="collection">Your collection of TaxMaster class to bulk import.</param>
        /// <returns>Returns list of imported taxMasterIds.</returns>
        /// <exception cref="MixERPException">Thrown when your any TaxMaster class in the collection is invalid or malformed.</exception>
        [AcceptVerbs("PUT")]
        [Route("bulk-import")]
        [Route("~/api/core/tax-master/bulk-import")]
        public List<object> BulkImport([FromBody]dynamic collection)
        {
            List<MixERP.Net.Entities.Core.TaxMaster> taxMasterCollection = this.ParseCollection(collection);

            if (taxMasterCollection == null || taxMasterCollection.Count.Equals(0))
            {
                return null;
            }

            try
            {
                return this.TaxMasterContext.BulkImport(taxMasterCollection);
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
        ///     Deletes an existing instance of TaxMaster class via TaxMasterId.
        /// </summary>
        /// <param name="taxMasterId">Enter the value for TaxMasterId in order to find and delete the existing record.</param>
        [AcceptVerbs("DELETE")]
        [Route("delete/{taxMasterId}")]
        [Route("~/api/core/tax-master/delete/{taxMasterId}")]
        public void Delete(int taxMasterId)
        {
            try
            {
                this.TaxMasterContext.Delete(taxMasterId);
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