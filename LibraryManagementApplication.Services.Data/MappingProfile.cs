using AutoMapper;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.ViewModels;


namespace LibraryManagementApplication.Services.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(model => model.AuthorName, opt => opt.MapFrom(source => source.Author.Name))
                .ForMember(model => model.GenreName, opt => opt.MapFrom(source => source.Genre.Name));

            CreateMap<BookViewModel, Book>()
                .ForMember(b => b.Author, opt => opt.Ignore())
                .ForMember(b => b.Genre, opt => opt.Ignore());
        }
    }
}
