using AutoMapper;
using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Infrastructure;
using System.Linq;

namespace DirectDebitApi.V1.Factories.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<DirectDebitDbEntity, DirectDebit>()

                .ForMember(dest => dest.BankOrBuildingSociety,
                           opt => opt.MapFrom(src => src.BankOrBuildingSociety))
                .ForMember(dest => dest.AccountHolders,
                           opt => opt.MapFrom(src => src.AccountHolders))
               .ReverseMap();
            CreateMap<DirectDebitMaintenanceDbEntity, DirectDebitMaintenance>().ReverseMap();
            CreateMap<DirectDebitSubmissionDbEntity, DirectDebitSubmission>().ReverseMap();


            CreateMap<DirectDebit, PTXSubmissionFileData>()

               .ForMember(dest => dest.Name,
                          opt => opt.MapFrom(src => src.AccountHolders.FirstOrDefault().Name))
               .ForMember(dest => dest.Ref,
                          opt => opt.MapFrom(src => src.PaymentReference))
                .ForMember(dest => dest.Sort,
                          opt => opt.MapFrom(src => src.BranchSortCode))
                  .ForMember(dest => dest.Number,
                          opt => opt.MapFrom(src => src.BankAccountNumber))
                  .ForMember(dest => dest.Amount,
                          opt => opt.ConvertUsing(new CurrencyFormatter(), src => (src.AdditionalAmount *100)))

              .ReverseMap();

        }

        private class CurrencyFormatter : IValueConverter<decimal, string>
        {
            public string Convert(decimal source, ResolutionContext context)
                => (source.ToString().PadLeft(11,'0'));
        }
    }
}