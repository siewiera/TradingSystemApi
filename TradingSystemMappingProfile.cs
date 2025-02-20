using AutoMapper;
using System.Xml;
using TradingSystemApi.Entities;
using TradingSystemApi.Models;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.BarcodeDto;
using TradingSystemApi.Models.CashierDto;
using TradingSystemApi.Models.Customer;
using TradingSystemApi.Models.InitData;
using TradingSystemApi.Models.Product;
using TradingSystemApi.Models.ProductCategory;
using TradingSystemApi.Models.SellerDto;
using TradingSystemApi.Models.Session;
using TradingSystemApi.Models.StoreDto;

namespace TradingSystemApi
{
    public class TradingSystemMappingProfile : Profile
    {
        public TradingSystemMappingProfile()
        {
            //Store
            CreateMap<Store, StoreDto>()
                .ForMember(sd => sd.SellerDto, s => s.MapFrom(s => s.Sellers))
                .ForMember(sd => sd.AdressDto, s => s.MapFrom(s => s.Adresses));

            CreateMap<AddNewStoreDto, Store>();
            CreateMap<InitDataDto, Store>()
                .ForMember(s => s.Adresses, a => a.MapFrom(dto => new List<Adress>
                {
                    new Adress
                    {
                        Street = dto.Street,
                        HouseNo = dto.HouseNo,
                        City = dto.City,
                        ZipCode = dto.ZipCode,
                        Country = dto.Country
                    }
                }))
                .ForMember(s => s.Sellers, i => i.MapFrom((dto, store) => new List<Seller>
                {
                    new Seller
                    {
                        Name = dto.SellerName,
                        TaxId = dto.TaxId,
                        Adress = store.Adresses.First(),
                        Cashiers = new List<Cashier>
                        {
                            new Cashier
                            {
                                Username = dto.Username,
                                Password = dto.Password,
                                UserRole = dto.UserRole,
                                CreatedAt = dto.CreatedAt,
                                Blocked = dto.Blocked,
                                Active = dto.Active,
                            }
                        }
                    }
                }));


            //Adress
            CreateMap<Adress, AdressDto>();
            CreateMap<AddingNewAdressDto, Adress>();
            CreateMap<UpdateAdressDto, Adress>();
            CreateMap<UpdateAdressDto, AddingNewAdressDto>();

            //Seller
            CreateMap<Seller, SellerDto>()
                .ForMember(sd => sd.AdressId, s => s.MapFrom(s => s.Adress.Id))
                .ForMember(sd => sd.Street, s => s.MapFrom(s => s.Adress.Street))
                .ForMember(sd => sd.HouseNo, s => s.MapFrom(s => s.Adress.HouseNo))
                .ForMember(sd => sd.City, s => s.MapFrom(s => s.Adress.City))
                .ForMember(sd => sd.ZipCode, s => s.MapFrom(s => s.Adress.ZipCode))
                .ForMember(sd => sd.Country, s => s.MapFrom(s => s.Adress.Country));
            CreateMap<AddSellerDetailsWithNewAdressDto, Seller>()
                .ForMember(s => s.Adress, a => a.MapFrom(dto =>
                    new Adress()
                    {
                        Street = dto.Street,
                        HouseNo = dto.HouseNo,
                        City = dto.City,
                        ZipCode = dto.ZipCode,
                        Country = dto.Country,
                    }));
            CreateMap<UpdateSellerDetailsDto, Seller>()
                .ForMember(s => s.AdressId, dto => dto.MapFrom(dto => dto.AdressId));
            CreateMap<AddSellerDetailsWithExistingtAdressDto, Seller>();

            //Cashier        
            CreateMap<AddingNewCashierDto, Cashier>();
            CreateMap<UpdateCashierDetailsDto, Cashier>();
            CreateMap<UpdateAdminAccountDto, Cashier>();
            CreateMap<Cashier, CashierDto>()
                .ForMember(cd => cd.SessionGuid, c => c.MapFrom(c => c.Session.SessionGuid))
                .ForMember(cd => cd.LoginTime, c => c.MapFrom(c => c.Session.LoginTime))
                .ForMember(cd => cd.LastAction, c => c.MapFrom(c => c.Session.LastAction))
                .ForMember(cd => cd.Ip, c => c.MapFrom(c => c.Session.Ip));


            //Customer
            CreateMap<Customer, CustomerDto>()
                .ForMember(cd => cd.AdressId, c => c.MapFrom(c => c.Adress.Id))
                .ForMember(cd => cd.Street, c => c.MapFrom(c => c.Adress.Street))
                .ForMember(cd => cd.HouseNo, c => c.MapFrom(c => c.Adress.HouseNo))
                .ForMember(cd => cd.City, c => c.MapFrom(c => c.Adress.City))
                .ForMember(cd => cd.ZipCode, c => c.MapFrom(c => c.Adress.ZipCode))
                .ForMember(cd => cd.Country, c => c.MapFrom(c => c.Adress.Country));
            CreateMap<AddCustomerDetailsWithExistingtAdressDto, Customer>();
            CreateMap<AddCustomerDetailsWithNewAdressDto, Customer>()
                .ForMember(c => c.Adress, a => a.MapFrom(dto =>
                    new Adress()
                    {
                        Street = dto.Street,
                        HouseNo = dto.HouseNo,
                        City = dto.City,
                        ZipCode = dto.ZipCode,
                        Country = dto.Country,
                    }
                ));
            CreateMap<UpdateCustomerDetailsDto, Customer>();


            //Barcode
            CreateMap<AddNewBarcodeDto, Barcode>();
            CreateMap<UpdateBarcodeDataDto, Barcode>();
            CreateMap<Barcode, BarcodeDto>();


            //ProductCategory
            CreateMap<AddNewProductCategoryDto, ProductCategory>();
            CreateMap<UpdateProductCategoryDataDto, ProductCategory>();
            CreateMap<ProductCategory, ProductCategoryDto>();

            //Product
            CreateMap<AddNewProductDto, Product>();
            CreateMap<UpdateProductDataDto, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(pd => pd.ProductCategoryName, p => p.MapFrom(p => p.ProductCategory.Name))
                .ForMember(pd => pd.BarcodeDtos, p => p.MapFrom(p => p.Barcodes));


            //Session
            CreateMap<Session, SessionDto>();

        }
    }
}
