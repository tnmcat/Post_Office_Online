using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Models;

namespace PostOffice.API.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            // any guid
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var employeeId = new Guid("59BD714F-9576-45BA-B5B7-F00649BE00DE");
            var customerId = new Guid("49BD714F-9576-45BA-B5B7-F00649BE00DE");


            var adminRoleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");                          
            var employeeRoleId = new Guid("79BD714F-9576-45BA-B5B7-F00649BE00DE");
            var customerRoleId = new Guid("0D04DCE2-969A-435D-BBA4-DF3F325983DC");
          
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = adminRoleId,
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "Administrator role"
            },
            new AppRole
            {
                Id = employeeRoleId,
                Name = "employee",
                NormalizedName = "EMPLOYEE",
                Description = "Employee role"
            },
            new AppRole
            {
                Id = customerRoleId,
                Name = "customer",
                NormalizedName = "CUSTOMER",
                Description = "Customer role"
            }
            );

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "onlinepostofficegroup4@gmail.com",
                NormalizedEmail = "onlinepostofficegroup4@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "aptech.123"),
                SecurityStamp = string.Empty,
                FirstName = "Pham",
                LastName = "Chien",
                PhoneNumber = "0950003946",
                PincodeId = "70000",
                Address = "150 Cong Hoa, Tan Binh District",
                Create_date = new DateTime(2019, 12, 01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });

           
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = employeeId,
                UserName = "hoanp",
                NormalizedUserName = "HOANP",
                Email = "nguyenphuonghoa0709@gmail.com",
                NormalizedEmail = "nguyenphuonghoa0709@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Hoa.12"),
                SecurityStamp = string.Empty,
                FirstName = "Nguyen",
                LastName = "Phuong Hoa",
                PhoneNumber = "0119703946",
                PincodeId = "70000",
                Address = "15 Nguyen Van Dau, Binh Thanh District",
                Create_date = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = employeeRoleId,
                UserId = employeeId
            });
           
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = customerId,
                UserName = "hoang",
                NormalizedUserName = "HOANG",
                Email = "hoanguyen@gmail.com",
                NormalizedEmail = "hoanguyen@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Hoa.13"),
                SecurityStamp = string.Empty,
                FirstName = "Nguyen",
                LastName = "Hoa",
                PhoneNumber = "0933739406",
                PincodeId = "70000",
                Address = "70 Cong Hoa, Tan Binh District",
                Create_date = new DateTime(2021, 07, 12)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = customerRoleId,
                UserId = customerId
            });


            modelBuilder.Entity<MoneyScope>().HasData(new MoneyScope
            {
                id = 1,
                min_value = 1,
                max_value = 1000000,
                description = "Under one million",

            },
               new MoneyScope
               {
                   id = 2,
                   min_value = 1000001,
                   max_value = 5000000,
                   description = "1 - 5 million",
               },
               new MoneyScope
               {
                   id = 3,
                   min_value = 5000001,
                   max_value = 20000000,
                   description = "5 -20 million",
               },
               new MoneyScope
               {
                   id = 4,
                   min_value = 20000001,
                   max_value = 50000000,
                   description = "20 -50 million",
               },
               new MoneyScope
               {
                   id = 5,
                   min_value = 50000001,
                   max_value = 100000000,
                   description = "50 - 100 million",
               });

            //area
            modelBuilder.Entity<WeightScope>().HasData(
                new WeightScope { id = 1, min_weight = 1, max_weight = 250, description = "0 - 250g" },
                new WeightScope { id = 2, min_weight = 251, max_weight = 1000, description = "250 - 1kg" },
                new WeightScope { id = 3, min_weight = 1001, max_weight = 10000, description = "1kg - 10kg" },
                new WeightScope { id = 4, min_weight = 10001, max_weight = 30000, description = "10kg - 30kg" },
                new WeightScope { id = 5, min_weight = 30001, max_weight = 100000, description = "Over 30kg" }
               );

            //parcel type
            modelBuilder.Entity<ParcelType>().HasData(
                new ParcelType { id = 1, max_length = 100, max_height = 100, max_width = 100, name = "Document", description = "Document", over_dimension_rate = 10} , 
                new ParcelType { id = 2, max_length = 300, max_height = 300, max_width = 300, name = "Merchandise", description = "Merchandise", over_dimension_rate = 15 }
                );

            //parcel service
            modelBuilder.Entity<ParcelService>().HasData(
                new ParcelService { service_id = 1, name = "Economy", description = "These services are cost-effective but might take longer for delivery, especially for longer distances.", status = true, delivery_time = 3 },
                new ParcelService { service_id = 2, name = "Express", description = "Fast delivery services that promise quick delivery, often within one day or overnight, regardless of distance.", status = true, delivery_time = 1 }
                );

            //parcel service price
            modelBuilder.Entity<ParcelServicePrice>().HasData(
               new ParcelServicePrice { parcel_price_id = 1, zone_type_id = 1, scope_weight_id = 1, service_id = 1, parcel_type_id =1,service_price = 10000 },
               new ParcelServicePrice { parcel_price_id = 2, zone_type_id = 2, scope_weight_id = 1, service_id = 1, parcel_type_id =1,service_price = 15000 },
               new ParcelServicePrice { parcel_price_id = 3, zone_type_id = 3, scope_weight_id = 1, service_id = 1, parcel_type_id =1,service_price = 22000 },
               new ParcelServicePrice { parcel_price_id = 4, zone_type_id = 1, scope_weight_id = 2, service_id = 1, parcel_type_id =1,service_price = 23000 },
               new ParcelServicePrice { parcel_price_id = 5, zone_type_id = 2, scope_weight_id = 2, service_id = 1, parcel_type_id =1,service_price = 27000 },
               new ParcelServicePrice { parcel_price_id = 6, zone_type_id = 3, scope_weight_id = 2, service_id = 1, parcel_type_id =1,service_price = 30000 },
               new ParcelServicePrice { parcel_price_id = 7, zone_type_id = 1, scope_weight_id = 3, service_id = 1, parcel_type_id =1,service_price = 30000 },
               new ParcelServicePrice { parcel_price_id = 8, zone_type_id = 2, scope_weight_id = 3, service_id = 1, parcel_type_id =1,service_price = 34000 },
               new ParcelServicePrice { parcel_price_id = 9, zone_type_id = 3, scope_weight_id = 3, service_id = 1, parcel_type_id =1,service_price = 44000 },
               new ParcelServicePrice { parcel_price_id = 10, zone_type_id = 1, scope_weight_id = 4,service_id = 1, parcel_type_id =1,service_price = 50000 },
               new ParcelServicePrice { parcel_price_id = 11, zone_type_id = 2, scope_weight_id = 4,service_id = 1, parcel_type_id =1,service_price = 55000 },
               new ParcelServicePrice { parcel_price_id = 12, zone_type_id = 3, scope_weight_id = 4,service_id = 1, parcel_type_id =1,service_price = 59000 },
               new ParcelServicePrice { parcel_price_id = 13, zone_type_id = 1, scope_weight_id = 5,service_id = 1, parcel_type_id =1,service_price = 60000 },
               new ParcelServicePrice { parcel_price_id = 14, zone_type_id = 2, scope_weight_id = 5,service_id = 1, parcel_type_id =1,service_price = 66000 },
               new ParcelServicePrice { parcel_price_id = 15, zone_type_id = 3, scope_weight_id = 5,service_id = 1, parcel_type_id =1,service_price = 70000 },
               new ParcelServicePrice { parcel_price_id = 16, zone_type_id = 1, scope_weight_id = 1, service_id = 1, parcel_type_id = 2, service_price = 15000 },
               new ParcelServicePrice { parcel_price_id = 17, zone_type_id = 2, scope_weight_id = 1, service_id = 1, parcel_type_id = 2, service_price = 17000 },
               new ParcelServicePrice { parcel_price_id = 18, zone_type_id = 3, scope_weight_id = 1, service_id = 1, parcel_type_id = 2, service_price = 28000 },
               new ParcelServicePrice { parcel_price_id = 19, zone_type_id = 1, scope_weight_id = 2, service_id = 1, parcel_type_id = 2, service_price = 29000 },
               new ParcelServicePrice { parcel_price_id = 20, zone_type_id = 2, scope_weight_id = 2, service_id = 1, parcel_type_id = 2, service_price = 21000 },
               new ParcelServicePrice { parcel_price_id = 21, zone_type_id = 3, scope_weight_id = 2, service_id = 1, parcel_type_id = 2, service_price = 32000 },
               new ParcelServicePrice { parcel_price_id = 22, zone_type_id = 1, scope_weight_id = 3, service_id = 1, parcel_type_id = 2, service_price = 34000 },
               new ParcelServicePrice { parcel_price_id = 23, zone_type_id = 2, scope_weight_id = 3, service_id = 1, parcel_type_id = 2, service_price = 35000 },
               new ParcelServicePrice { parcel_price_id = 24, zone_type_id = 3, scope_weight_id = 3, service_id = 1, parcel_type_id = 2, service_price = 46000 },
               new ParcelServicePrice { parcel_price_id = 25, zone_type_id = 1, scope_weight_id = 4, service_id = 2, parcel_type_id = 2, service_price = 50000 },
               new ParcelServicePrice { parcel_price_id = 26, zone_type_id = 2, scope_weight_id = 4, service_id = 2, parcel_type_id = 2, service_price = 55000 },
               new ParcelServicePrice { parcel_price_id = 27, zone_type_id = 3, scope_weight_id = 4, service_id = 2, parcel_type_id = 2, service_price = 59000 },
               new ParcelServicePrice { parcel_price_id = 28, zone_type_id = 1, scope_weight_id = 5, service_id = 2, parcel_type_id = 2, service_price = 60000 },
               new ParcelServicePrice { parcel_price_id = 29, zone_type_id = 2, scope_weight_id = 5, service_id = 2, parcel_type_id = 2, service_price = 66000 },
               new ParcelServicePrice { parcel_price_id = 30, zone_type_id = 3, scope_weight_id = 5, service_id = 2, parcel_type_id = 2, service_price = 70000 },
               new ParcelServicePrice { parcel_price_id = 31, zone_type_id = 1, scope_weight_id = 4, service_id = 2, parcel_type_id = 1, service_price = 50000 },
               new ParcelServicePrice { parcel_price_id = 32, zone_type_id = 2, scope_weight_id = 4, service_id = 2, parcel_type_id = 1, service_price = 59000 },
               new ParcelServicePrice { parcel_price_id = 33, zone_type_id = 3, scope_weight_id = 4, service_id = 2, parcel_type_id = 1, service_price = 59000 },
               new ParcelServicePrice { parcel_price_id = 34, zone_type_id = 1, scope_weight_id = 5, service_id = 2, parcel_type_id = 1, service_price = 60000 },
               new ParcelServicePrice { parcel_price_id = 35, zone_type_id = 2, scope_weight_id = 5, service_id = 2, parcel_type_id = 1, service_price = 66000 },
               new ParcelServicePrice { parcel_price_id = 36, zone_type_id = 3, scope_weight_id = 5, service_id = 2, parcel_type_id = 1, service_price = 70000 }

           );

            modelBuilder.Entity<MoneyServicePrice>().HasData(
            new MoneyServicePrice { id = 1, zone_type_id = 1, money_scope_id = 1, fee = 50000},
            new MoneyServicePrice { id = 2, zone_type_id = 2, money_scope_id = 1, fee = 75000 },
            new MoneyServicePrice { id = 3, zone_type_id = 3, money_scope_id = 1, fee = 100000 },
            new MoneyServicePrice { id = 4, zone_type_id = 1, money_scope_id = 2, fee = 100000 },
            new MoneyServicePrice { id = 5, zone_type_id = 2, money_scope_id = 2, fee = 125000 },
            new MoneyServicePrice { id = 6, zone_type_id = 3, money_scope_id = 2, fee = 150000 },
            new MoneyServicePrice { id = 7, zone_type_id = 1, money_scope_id = 3, fee = 150000 },
            new MoneyServicePrice { id = 8, zone_type_id = 2, money_scope_id = 3, fee = 175000 },
            new MoneyServicePrice { id = 9, zone_type_id = 3, money_scope_id = 3, fee = 200000 },
            new MoneyServicePrice { id = 10, zone_type_id = 1, money_scope_id = 4, fee = 200000 },
            new MoneyServicePrice { id = 11, zone_type_id = 2, money_scope_id = 4, fee = 225000 },
            new MoneyServicePrice { id = 12, zone_type_id = 3, money_scope_id = 4, fee = 250000 },
            new MoneyServicePrice { id = 13, zone_type_id = 1, money_scope_id = 5, fee = 250000 },
            new MoneyServicePrice { id = 14, zone_type_id = 2, money_scope_id = 5, fee = 275000 },
            new MoneyServicePrice { id = 15, zone_type_id = 3, money_scope_id = 5, fee = 300000 }
            );
            //area
            modelBuilder.Entity<ZoneType>().HasData(
                new ZoneType { id = 1, zone_description = "Local" },
                new ZoneType { id = 2, zone_description = "Regional" },
                new ZoneType { id = 3, zone_description = "National" }
               );
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Status = "Completed" },
                new OrderStatus { Id = 2, Status = "Shipping" },
                new OrderStatus { Id = 3, Status = "Pending" }
                );
            //area
            modelBuilder.Entity<Area>().HasData(
                new Area { id = 1, area_name = "The North" },
                new Area { id = 2, area_name = "The Central" },
                new Area { id = 3, area_name =  "The South" }
               );
            //pincode
            modelBuilder.Entity<Pincode>().HasData(
                new Pincode { pincode = "90000", city_name = "An Giang", area_id = 3 },
                new Pincode { pincode = "26000", city_name = "Bắc Giang", area_id = 1 },
                new Pincode { pincode = "23000", city_name = "Bắc Kạn", area_id = 1 },
                new Pincode { pincode = "97000", city_name = "Bạc Liêu", area_id = 3 },
                new Pincode { pincode = "16000", city_name = "Bắc Ninh", area_id = 1 },
                new Pincode { pincode = "78000", city_name = "Bà Rịa–Vũng Tàu", area_id = 3 },
                new Pincode { pincode = "86000", city_name = "Bến Tre", area_id = 3 },
                new Pincode { pincode = "55000", city_name = "Bình Định", area_id = 2 },
                new Pincode { pincode = "75000", city_name = "Bình Dương", area_id = 3 },
                new Pincode { pincode = "67000", city_name = "Bình Phước", area_id = 3 },
                new Pincode { pincode = "77000", city_name = "Bình Thuận", area_id = 2 },
                new Pincode { pincode = "98000", city_name = "Cà Mau", area_id = 3 },
                new Pincode { pincode = "94000", city_name = "Cần Thơ", area_id = 3 },
                new Pincode { pincode = "21000", city_name = "Cao Bằng", area_id = 1 },
                new Pincode { pincode = "50000", city_name = "Đà Nẵng", area_id = 2 },
                new Pincode { pincode = "63000", city_name = "Đắk Lắk", area_id = 3 },
                new Pincode { pincode = "65000", city_name = "Đắk Nông", area_id = 3 },
                new Pincode { pincode = "32000", city_name = "Điện Biên", area_id = 1 },
                new Pincode { pincode = "76000", city_name = "Đồng Nai", area_id = 3 },
                new Pincode { pincode = "81000", city_name = "Đồng Tháp", area_id = 3 },
                new Pincode { pincode = "61000", city_name = "Gia Lai", area_id = 3 },
                new Pincode { pincode = "20000", city_name = "Hà Giang", area_id = 1 },
                new Pincode { pincode = "18000", city_name = "Hà Nam", area_id = 1 },
                new Pincode { pincode = "45000", city_name = "Hà Tĩnh", area_id = 1 },
                new Pincode { pincode = "03000", city_name = "Hải Dương", area_id = 1 },
                new Pincode { pincode = "04000", city_name = "Hải Phòng", area_id = 1 },
                new Pincode { pincode = "10000", city_name = "Hà Nội", area_id = 1 },
                new Pincode { pincode = "95000", city_name = "Hậu Giang", area_id = 3 },
                new Pincode { pincode = "36000", city_name = "Hòa Bình", area_id = 1 },
                new Pincode { pincode = "70000", city_name = "Hồ Chí Minh (TP)", area_id = 3 },
                new Pincode { pincode = "17000", city_name = "Hưng Yên", area_id = 1 },
                new Pincode { pincode = "57000", city_name = "Khánh Hòa", area_id = 2 },
                new Pincode { pincode = "91000", city_name = "Kiên Giang", area_id = 3 },
                new Pincode { pincode = "60000", city_name = "Kon Tum", area_id = 3 },
                new Pincode { pincode = "30000", city_name = "Lai Châu", area_id = 1 },
                new Pincode { pincode = "66000", city_name = "Lâm Đồng", area_id = 3 },
                new Pincode { pincode = "25000", city_name = "Lạng Sơn", area_id = 1 },
                new Pincode { pincode = "31000", city_name = "Lào Cai", area_id = 1 },
                new Pincode { pincode = "82000", city_name = "Long An", area_id = 3 },
                new Pincode { pincode = "07000", city_name = "Nam Định", area_id = 1 },
                new Pincode { pincode = "43000", city_name = "Nghệ An", area_id = 1 },
                new Pincode { pincode = "08000", city_name = "Ninh Bình", area_id = 1 },
                new Pincode { pincode = "59000", city_name = "Ninh Thuận", area_id = 2 },
                new Pincode { pincode = "35000", city_name = "Phú Thọ", area_id = 1 },
                new Pincode { pincode = "56000", city_name = "Phú Yên", area_id = 2 },
                new Pincode { pincode = "47000", city_name = "Quảng Bình", area_id = 1 },
                new Pincode { pincode = "51000", city_name = "Quảng Nam", area_id = 2 },
                new Pincode { pincode = "53000", city_name = "Quảng Ngãi", area_id = 2 },
                new Pincode { pincode = "01000", city_name = "Quảng Ninh", area_id = 1 },
                new Pincode { pincode = "48000", city_name = "Quảng Trị", area_id = 2 },
                new Pincode { pincode = "96000", city_name = "Sóc Trăng", area_id = 3 },
                new Pincode { pincode = "34000", city_name = "Sơn La", area_id = 1 },
                new Pincode { pincode = "80000", city_name = "Tây Ninh", area_id = 3 },
                new Pincode { pincode = "06000", city_name = "Thái Bình", area_id = 1 },
                new Pincode { pincode = "24000", city_name = "Thái Nguyên", area_id = 1 },
                new Pincode { pincode = "40000", city_name = "Thanh Hóa", area_id = 1 },
                new Pincode { pincode = "49000", city_name = "Thừa Thiên–Huế", area_id = 2 },
                new Pincode { pincode = "84000", city_name = "Tiền Giang", area_id = 3 },
                new Pincode { pincode = "87000", city_name = "Trà Vinh", area_id = 3 },
                new Pincode { pincode = "22000", city_name = "Tuyên Quang", area_id = 1 },
                new Pincode { pincode = "85000", city_name = "Vĩnh Long", area_id = 3 },
                new Pincode { pincode = "15000", city_name = "Vĩnh Phúc", area_id = 1 },
                new Pincode { pincode = "33000", city_name = "Yên Bái", area_id = 1 }

              );

            //branch
            modelBuilder.Entity<OfficeBranch>().HasData(
               new OfficeBranch
               {
                  id = "716040" ,
                  branch_name = "Phuoc Binh",
                  pincode = "70000",
                  district_name ="Distric 9",
                  address = "60 Phuoc Binh",
                  branch_phone = "37281646"
               },
               new OfficeBranch
               {
                   id = "720300",
                   branch_name = "Thi Nghe",
                   pincode = "70000",
                   district_name = "Binh Thanh District",
                   address = "23 Xo Viet Nghe Tinh, Ward 17",
                   branch_phone = "37281646"
               }
            );

            //money order
            modelBuilder.Entity<MoneyOrder>().HasData(
                new MoneyOrder
                {
                    id = 1, 
                    user_id = new Guid("49BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    sender_name = "Tran Thi Binh",
                    sender_pincode = "40000",
                    sender_address = "40 Nguyen Ba Ngoc, Ba Dinh District",
                    sender_phone = "023591330",
                    sender_email = "ttbinh@gmail.com",
                    sender_national_identity_number = "0256214637",


                    receiver_name = "Le Van Bay",
                    receiver_pincode = "70000",
                    receiver_address = "105 Cong Hoa, Tan Binh District",
                    receiver_phone = "055591370",
                    receiver_email = "lvbay@gmail.com",
                    receiver_national_identity_number = "0789262637",

                    transfer_status = Enums.TransferStatus.Pending,
                    note ="pay for home rental",
                    transfer_value = 30000000,
                    transfer_fee = 15000,

                    payer = "sender",
                    send_date = new DateTime(2022, 07, 10),
                    total_charge = 30015000
                }
                );
            //parcel order
            modelBuilder.Entity<ParcelOrder>().HasData(
                new ParcelOrder
                {
                    id = 1,
                    user_id = new Guid("49BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    sender_name = "Tran Van Quang",
                    sender_pincode = "40000",
                    sender_address = "40 Nguyen Ba Ngoc, Ward 5, Ba Dinh District",
                    sender_phone = "023591330",
                    sender_email = "ttbinh@gmail.com",                   


                    receiver_name = "Le Van Bay",
                    receiver_pincode = "70000",
                    receiver_address = "100 Truong Chinh, Ward 11, Tan Binh District",
                    receiver_phone = "097631370",
                    receiver_email = "lvbay@gmail.com",

                    description = "2 books, 5 pencils",
                    note = "birthday presents",
                    parcel_length = 20,
                    parcel_width = 30, 
                    parcel_height =25,
                    parcel_weight = 1000,
                    service_id = 1,
                    parcel_type_id = 2,

                    payer = "sender",
                    payment_method = "Cash",
                    vpp_value = 0,
                    send_date = new DateTime(2023, 01, 14),
                    receive_date = new DateTime(2023, 01, 18),
                    order_status = 1,
                    total_charge = 36000
                }
                );
            modelBuilder.Entity<TrackHistory>().HasData(
                new TrackHistory
                {
                    track_id = 1,
                    order_id = 1,
                    employee_id = employeeId,
                    new_status = "on delivery",
                    update_time = new DateTime(2023, 01, 15) ,
                    new_location = "Nha Trang"
                });

        }
    }
}

