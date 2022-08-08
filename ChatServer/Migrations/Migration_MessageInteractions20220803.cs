using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(12)]
    public class Migration_MessageInteractions20220803 : Migration
    {
        public override void Down(){
            Delete.Table("message_interaction");
        }

        public override void Up(){
            Create.Table("message_interaction")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(1,1)
                .WithColumn("message").AsInt32().NotNullable()
                .WithColumn("user").AsInt32().NotNullable()
                .WithColumn("interaction").AsInt32().NotNullable();
        }
    }
}