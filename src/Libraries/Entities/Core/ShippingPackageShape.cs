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
// ReSharper disable All
using PetaPoco;
using System;

namespace MixERP.Net.Entities.Core
{
    [PrimaryKey("shipping_package_shape_id", autoIncrement = true)]
    [TableName("core.shipping_package_shapes")]
    [ExplicitColumns]
    public sealed class ShippingPackageShape : PetaPocoDB.Record<ShippingPackageShape>, IPoco
    {
        [Column("shipping_package_shape_id")]
        [ColumnDbType("int4", 0, false, "nextval('core.shipping_package_shapes_shipping_package_shape_id_seq'::regclass)")]
        public int ShippingPackageShapeId { get; set; }

        [Column("shipping_package_shape_code")]
        [ColumnDbType("varchar", 12, false, "")]
        public string ShippingPackageShapeCode { get; set; }

        [Column("shipping_package_shape_name")]
        [ColumnDbType("varchar", 64, false, "")]
        public string ShippingPackageShapeName { get; set; }

        [Column("is_rectangular")]
        [ColumnDbType("bool", 0, false, "false")]
        public bool IsRectangular { get; set; }

        [Column("audit_user_id")]
        [ColumnDbType("int4", 0, true, "")]
        public int? AuditUserId { get; set; }

        [Column("audit_ts")]
        [ColumnDbType("timestamptz", 0, true, "")]
        public DateTime? AuditTs { get; set; }
    }
}