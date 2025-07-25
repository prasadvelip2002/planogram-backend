﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanogramBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddShelfRowToPlanogramGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelfRow",
                table: "PlanogramGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelfRow",
                table: "PlanogramGroups");
        }
    }
}
