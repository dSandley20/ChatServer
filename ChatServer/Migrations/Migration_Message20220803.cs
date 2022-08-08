using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(9)]
    public class Migration_Message20220803 : Migration
    {
        public override void Down(){
            Delete.Table("message");
        }

        public override void Up(){
            Create.Table("message")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity(1,1)
                .WithColumn("user").AsInt32().NotNullable()
                .WithColumn("server").AsInt32().NotNullable()
                .WithColumn("content").AsString();
        }
    }
}