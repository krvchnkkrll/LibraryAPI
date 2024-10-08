﻿#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BorrowPeriod",
                table: "UserBooks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowPeriod",
                table: "UserBooks");
        }
    }
}
