using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(10)]
    public class Migration_Interaction20220803 : Migration
    {
        public override void Down() {
            Delete.Table("interaction");
        }

        public override void Up(){
            Create.Table("interaction")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(1,1)
                .WithColumn("emoji").AsString().NotNullable();
        }
    }
}