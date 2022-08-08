using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(7)]
    public class Migration_UserLocationForeignKeyUser20220801 : Migration
    {
        public override void Down(){
            Delete.ForeignKey();
        }

        public override void Up(){
             Create.ForeignKey()
                    .FromTable("user_location").ForeignColumn("user")
                    .ToTable("users").PrimaryColumn("id");
        }
    }
}