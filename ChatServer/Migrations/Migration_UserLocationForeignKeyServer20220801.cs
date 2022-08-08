using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(8)]
    public class Migration_UserLocationForeignKeyServer20220801 : Migration
    {
        public override void Down(){
              Delete.ForeignKey();
        }

        public override void Up()
        {
            Create.ForeignKey()
                   .FromTable("user_location").ForeignColumn("server")
                   .ToTable("server").PrimaryColumn("id");
        }
    }
}