using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(6)]
    public class Migration_UserLocation20220801 : Migration
    {
         public override void Down()
        {
            Delete.Table("user_location");
        }

        public override void Up(){
            Create.Table("user_location")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity(1,1)
                .WithColumn("user").AsInt32().NotNullable()
                .WithColumn("server").AsInt32().NotNullable();
        }
    }
}