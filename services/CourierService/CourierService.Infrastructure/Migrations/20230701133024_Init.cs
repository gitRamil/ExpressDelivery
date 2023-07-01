using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourierService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_statuses", x => x.id);
                },
                comment: "Статус заказа");

            migrationBuilder.CreateTable(
                name: "package_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false, comment: "Цена посылки"),
                    short_description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Краткое описание"),
                    weight = table.Column<int>(type: "integer", nullable: false, comment: "Вес посылки"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_package_information", x => x.id);
                },
                comment: "Информация о посылке");

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_methods", x => x.id);
                },
                comment: "Метод оплаты заказа");

            migrationBuilder.CreateTable(
                name: "rights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Код"),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Наименование"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rights", x => x.id);
                },
                comment: "Права");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Имя"),
                    last_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true, comment: "Логин сотрудника"),
                    login = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Логин сотрудника"),
                    mail = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Эл. почта"),
                    password_hash = table.Column<byte[]>(type: "bytea", nullable: false, comment: "Хеш пароля"),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false, comment: "Соль пароля"),
                    phone = table.Column<string>(type: "text", nullable: true, comment: "Номер телефона"),
                    refresh_token = table.Column<string>(type: "text", nullable: false, comment: "Рефреш-токен"),
                    right_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор прав пользователя"),
                    token_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания токена"),
                    token_expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата истечения токена"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_rights_right_id",
                        column: x => x.right_id,
                        principalTable: "rights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Пользователи");

            migrationBuilder.CreateTable(
                name: "couriers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    latitude = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true, comment: "Координаты широты"),
                    longitude = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true, comment: "Координаты долготы"),
                    telegram_user_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Ник телеграмм"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_couriers", x => x.id);
                    table.ForeignKey(
                        name: "fk_couriers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Курьеры");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    courier_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор курьера"),
                    delivery_cost = table.Column<decimal>(type: "numeric", nullable: true, comment: "Цена доставки"),
                    delivery_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата доставки"),
                    order_status_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор статуса заказа"),
                    package_information_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор посылки"),
                    payment_method_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор метода оплаты"),
                    receiver_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор получателя"),
                    receiver_address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Адрес получателя"),
                    receiver_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Имя получателя"),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор связанной цели"),
                    sender_address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Адрес отправителя"),
                    sender_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false, comment: "Имя отправителя"),
                    track_number = table.Column<Guid>(type: "uuid", nullable: false, comment: "Номер отслеживания"),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_courier_courier_temp_id",
                        column: x => x.courier_id,
                        principalTable: "couriers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_order_status_order_status_temp_id",
                        column: x => x.order_status_id,
                        principalTable: "order_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_package_information_package_information_id",
                        column: x => x.package_information_id,
                        principalTable: "package_information",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_orders_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id");
                },
                comment: "Заказ");

            migrationBuilder.InsertData(
                table: "order_statuses",
                columns: new[] { "id", "code", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { new Guid("32ba2971-2a5e-435b-87c7-f8022e901c63"), "in_progress", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Заказ в процессе", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("4fdc6d99-f3fd-49ee-8af9-6ac5531cc40e"), "courier_assigned", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Курьер назначен", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("9171b0ee-7091-4dee-95aa-59c5522a21fd"), "done", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Заказ завершен", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("b63c138c-c36b-4bb1-8dad-b3770512b858"), "created", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Заказ создан", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "code", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { new Guid("424b93cd-ca77-4bb5-b20b-e0f1201bc350"), "online", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Онлайн", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("7373f370-6206-41c7-b4e7-91caddf1a35a"), "card", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Карта", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("d353d9a8-b9e2-4b8e-9207-e898ef328b52"), "cash", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Наличные", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "rights",
                columns: new[] { "id", "code", "created_date", "name", "updated_date" },
                values: new object[,]
                {
                    { new Guid("3dfcd6f3-1775-4e1b-91db-fdccea3f83eb"), "admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Администратор", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("60eb98f3-9f8c-4c12-93d4-66f208caa6f6"), "courier", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Курьер", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("e10222c4-7723-498b-8bf4-83252378e0c9"), "user", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Пользователь", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_couriers_user_id",
                table: "couriers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_statuses_code",
                table: "order_statuses",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_courier_id",
                table: "orders",
                column: "courier_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_order_status_id",
                table: "orders",
                column: "order_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_package_information_id",
                table: "orders",
                column: "package_information_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_payment_method_id",
                table: "orders",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_receiver_id",
                table: "orders",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_sender_id",
                table: "orders",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_methods_code",
                table: "payment_methods",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rights_code",
                table: "rights",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_right_id",
                table: "users",
                column: "right_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "couriers");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "package_information");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rights");
        }
    }
}
