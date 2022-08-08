using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(11)]
    public class Migration_PopulateInteraction20220803112300 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("interaction").AllRows();
        }

        public override void Up()
        {
            Insert.IntoTable("interaction").Row(new { emoji = "2639" });
        }
    }
}
