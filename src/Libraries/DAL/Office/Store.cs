/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using MixERP.Net.EntityParser;
using MixERP.Net.Framework;
using Npgsql;
using PetaPoco;
using Serilog;

namespace MixERP.Net.Schemas.Office.Data
{
    /// <summary>
    /// Provides simplified data access features to perform SCRUD operation on the database table "office.stores".
    /// </summary>
    public class Store : DbAccess
    {
        /// <summary>
        /// The schema of this table. Returns literal "office".
        /// </summary>
        public override string ObjectNamespace => "office";

        /// <summary>
        /// The schema unqualified name of this table. Returns literal "stores".
        /// </summary>
        public override string ObjectName => "stores";

        /// <summary>
        /// Login id of application user accessing this table.
        /// </summary>
        public long LoginId { get; set; }

        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string Catalog { get; set; }

        /// <summary>
        /// Performs SQL count on the table "office.stores".
        /// </summary>
        /// <returns>Returns the number of rows of the table "office.stores".</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long Count()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"Store\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT COUNT(*) FROM office.stores;";
            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Executes a select query on the table "office.stores" to return a all instances of the "Store" class to export. 
        /// </summary>
        /// <returns>Returns a non-live, non-mapped instances of "Store" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> Get()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.ExportData, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the export entity \"Store\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM office.stores ORDER BY store_id;";
            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql);
        }

        /// <summary>
        /// Executes a select query on the table "office.stores" with a where filter on the column "store_id" to return a single instance of the "Store" class. 
        /// </summary>
        /// <param name="storeId">The column "store_id" parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped instance of "Store" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public MixERP.Net.Entities.Office.Store Get(int storeId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the get entity \"Store\" filtered by \"StoreId\" with value {StoreId} was denied to the user with Login ID {LoginId}", storeId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM office.stores WHERE store_id=@0;";
            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql, storeId).FirstOrDefault();
        }

        /// <summary>
        /// Executes a select query on the table "office.stores" with a where filter on the column "store_id" to return a multiple instances of the "Store" class. 
        /// </summary>
        /// <param name="storeIds">Array of column "store_id" parameter used on where filter.</param>
        /// <returns>Returns a non-live, non-mapped collection of "Store" class mapped to the database row.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> Get(int[] storeIds)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to entity \"Store\" was denied to the user with Login ID {LoginId}. storeIds: {storeIds}.", this.LoginId, storeIds);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM office.stores WHERE store_id IN (@0);";

            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql, storeIds);
        }

        /// <summary>
        /// Custom fields are user defined form elements for office.stores.
        /// </summary>
        /// <returns>Returns an enumerable custom field collection for the table office.stores</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<PetaPoco.CustomField> GetCustomFields(string resourceId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get custom fields for entity \"Store\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            string sql;
            if (string.IsNullOrWhiteSpace(resourceId))
            {
                sql = "SELECT * FROM core.custom_field_definition_view WHERE table_name='office.stores' ORDER BY field_order;";
                return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql);
            }

            sql = "SELECT * from core.get_custom_field_definition('office.stores'::text, @0::text) ORDER BY field_order;";
            return Factory.Get<PetaPoco.CustomField>(this.Catalog, sql, resourceId);
        }

        /// <summary>
        /// Displayfields provide a minimal name/value context for data binding the row collection of office.stores.
        /// </summary>
        /// <returns>Returns an enumerable name and value collection for the table office.stores</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<DisplayField> GetDisplayFields()
        {
            List<DisplayField> displayFields = new List<DisplayField>();

            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return displayFields;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to get display field for entity \"Store\" was denied to the user with Login ID {LoginId}", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT store_id AS key, store_code || ' (' || store_name || ')' as value FROM office.stores;";
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                using (DataTable table = DbOperation.GetDataTable(this.Catalog, command))
                {
                    if (table?.Rows == null || table.Rows.Count == 0)
                    {
                        return displayFields;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        if (row != null)
                        {
                            DisplayField displayField = new DisplayField
                            {
                                Key = row["key"].ToString(),
                                Value = row["value"].ToString()
                            };

                            displayFields.Add(displayField);
                        }
                    }
                }
            }

            return displayFields;
        }

        /// <summary>
        /// Inserts or updates the instance of Store class on the database table "office.stores".
        /// </summary>
        /// <param name="store">The instance of "Store" class to insert or update.</param>
        /// <param name="customFields">The custom field collection.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void AddOrEdit(MixERP.Net.Entities.Office.Store store, List<EntityParser.CustomField> customFields)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            object primaryKeyValue;

            store.AuditUserId = this.UserId;
            store.AuditTs = System.DateTime.UtcNow;

            if (store.StoreId > 0)
            {
                primaryKeyValue = store.StoreId;
                this.Update(store, store.StoreId);
            }
            else
            {
                primaryKeyValue = this.Add(store);
            }

            string sql = "DELETE FROM core.custom_fields WHERE custom_field_setup_id IN(" +
                         "SELECT custom_field_setup_id " +
                         "FROM core.custom_field_setup " +
                         "WHERE form_name=core.get_custom_field_form_name('office.stores')" +
                         ");";

            Factory.NonQuery(this.Catalog, sql);

            foreach (var field in customFields)
            {
                sql = "INSERT INTO core.custom_fields(custom_field_setup_id, resource_id, value) " +
                      "SELECT core.get_custom_field_setup_id_by_table_name('office.stores', @0::character varying(100)), " +
                      "@1, @2;";

                Factory.NonQuery(this.Catalog, sql, field.FieldName, primaryKeyValue, field.Value);
            }
        }

        /// <summary>
        /// Inserts the instance of Store class on the database table "office.stores".
        /// </summary>
        /// <param name="store">The instance of "Store" class to insert.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public object Add(MixERP.Net.Entities.Office.Store store)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Create, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to add entity \"Store\" was denied to the user with Login ID {LoginId}. {Store}", this.LoginId, store);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            return Factory.Insert(this.Catalog, store);
        }

        /// <summary>
        /// Inserts or updates multiple instances of Store class on the database table "office.stores";
        /// </summary>
        /// <param name="stores">List of "Store" class to import.</param>
        /// <returns></returns>
        public List<object> BulkImport(List<MixERP.Net.Entities.Office.Store> stores)
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.ImportData, this.LoginId, false);
                }

                if (!this.HasAccess)
                {
                    Log.Information("Access to import entity \"Store\" was denied to the user with Login ID {LoginId}. {stores}", this.LoginId, stores);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            var result = new List<object>();
            int line = 0;
            try
            {
                using (Database db = new Database(Factory.GetConnectionString(this.Catalog), Factory.ProviderName))
                {
                    using (Transaction transaction = db.GetTransaction())
                    {
                        foreach (var store in stores)
                        {
                            line++;

                            store.AuditUserId = this.UserId;
                            store.AuditTs = System.DateTime.UtcNow;

                            if (store.StoreId > 0)
                            {
                                result.Add(store.StoreId);
                                db.Update(store, store.StoreId);
                            }
                            else
                            {
                                result.Add(db.Insert(store));
                            }
                        }

                        transaction.Complete();
                    }

                    return result;
                }
            }
            catch (NpgsqlException ex)
            {
                string errorMessage = $"Error on line {line} ";

                if (ex.Code.StartsWith("P"))
                {
                    errorMessage += Factory.GetDBErrorResource(ex);

                    throw new MixERPException(errorMessage, ex);
                }

                errorMessage += ex.Message;
                throw new MixERPException(errorMessage, ex);
            }
            catch (System.Exception ex)
            {
                string errorMessage = $"Error on line {line} ";
                throw new MixERPException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Updates the row of the table "office.stores" with an instance of "Store" class against the primary key value.
        /// </summary>
        /// <param name="store">The instance of "Store" class to update.</param>
        /// <param name="storeId">The value of the column "store_id" which will be updated.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void Update(MixERP.Net.Entities.Office.Store store, int storeId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Edit, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to edit entity \"Store\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}. {Store}", storeId, this.LoginId, store);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            Factory.Update(this.Catalog, store, storeId);
        }

        /// <summary>
        /// Deletes the row of the table "office.stores" against the primary key value.
        /// </summary>
        /// <param name="storeId">The value of the column "store_id" which will be deleted.</param>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public void Delete(int storeId)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Delete, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to delete entity \"Store\" with Primary Key {PrimaryKey} was denied to the user with Login ID {LoginId}.", storeId, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "DELETE FROM office.stores WHERE store_id=@0;";
            Factory.NonQuery(this.Catalog, sql, storeId);
        }

        /// <summary>
        /// Performs a select statement on table "office.stores" producing a paged result of 25.
        /// </summary>
        /// <returns>Returns the first page of collection of "Store" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> GetPagedResult()
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the first page of the entity \"Store\" was denied to the user with Login ID {LoginId}.", this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            const string sql = "SELECT * FROM office.stores ORDER BY store_id LIMIT 25 OFFSET 0;";
            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a select statement on table "office.stores" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <returns>Returns collection of "Store" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> GetPagedResult(long pageNumber)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the entity \"Store\" was denied to the user with Login ID {LoginId}.", pageNumber, this.LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            const string sql = "SELECT * FROM office.stores ORDER BY store_id LIMIT 25 OFFSET @0;";

            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql, offset);
        }

        private List<EntityParser.Filter> GetFilters(string catalog, string filterName)
        {
            const string sql = "SELECT * FROM core.filters WHERE object_name='office.stores' AND lower(filter_name)=lower(@0);";
            return Factory.Get<EntityParser.Filter>(catalog, sql, filterName).ToList();
        }

        /// <summary>
        /// Performs a filtered count on table "office.stores".
        /// </summary>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns number of rows of "Store" class using the filter.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long CountWhere(List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"Store\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            Sql sql = Sql.Builder.Append("SELECT COUNT(*) FROM office.stores WHERE 1 = 1");
            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Office.Store(), filters);

            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered select statement on table "office.stores" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filters">The list of filter conditions.</param>
        /// <returns>Returns collection of "Store" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> GetWhere(long pageNumber, List<EntityParser.Filter> filters)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"Store\" was denied to the user with Login ID {LoginId}. Filters: {Filters}.", pageNumber, this.LoginId, filters);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM office.stores WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Office.Store(), filters);

            sql.OrderBy("store_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered count on table "office.stores".
        /// </summary>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns number of rows of "Store" class using the filter.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public long CountFiltered(string filterName)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return 0;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to count entity \"Store\" was denied to the user with Login ID {LoginId}. Filter: {Filter}.", this.LoginId, filterName);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            List<EntityParser.Filter> filters = this.GetFilters(this.Catalog, filterName);
            Sql sql = Sql.Builder.Append("SELECT COUNT(*) FROM office.stores WHERE 1 = 1");
            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Office.Store(), filters);

            return Factory.Scalar<long>(this.Catalog, sql);
        }

        /// <summary>
        /// Performs a filtered select statement on table "office.stores" producing a paged result of 25.
        /// </summary>
        /// <param name="pageNumber">Enter the page number to produce the paged result.</param>
        /// <param name="filterName">The named filter.</param>
        /// <returns>Returns collection of "Store" class.</returns>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<MixERP.Net.Entities.Office.Store> GetFiltered(long pageNumber, string filterName)
        {
            if (string.IsNullOrWhiteSpace(this.Catalog))
            {
                return null;
            }

            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Read, this.LoginId, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to Page #{Page} of the filtered entity \"Store\" was denied to the user with Login ID {LoginId}. Filter: {Filter}.", pageNumber, this.LoginId, filterName);
                    throw new UnauthorizedException("Access is denied.");
                }
            }

            List<EntityParser.Filter> filters = this.GetFilters(this.Catalog, filterName);

            long offset = (pageNumber - 1) * 25;
            Sql sql = Sql.Builder.Append("SELECT * FROM office.stores WHERE 1 = 1");

            MixERP.Net.EntityParser.Data.Service.AddFilters(ref sql, new MixERP.Net.Entities.Office.Store(), filters);

            sql.OrderBy("store_id");
            sql.Append("LIMIT @0", 25);
            sql.Append("OFFSET @0", offset);

            return Factory.Get<MixERP.Net.Entities.Office.Store>(this.Catalog, sql);
        }

    }
}