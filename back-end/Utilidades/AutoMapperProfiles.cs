using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Consul;
using Microsoft.AspNetCore.Identity;
using Microsoft.Spatial;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<Color, colorDTO>().ReverseMap();
         
            CreateMap<Marca, MarcaDTO>().ReverseMap();
            CreateMap<talla, TallaDTO>().ReverseMap();
            CreateMap<talla, TallaCreacionDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<productos, ProductoDTO>().ReverseMap();




            CreateMap<ProductoCreacionDTO, productos>()
             .ForMember(x => x.productotalla, options => options.MapFrom(MapearProductotallacreaciontalla))
             .ForMember(x => x.productocolor, options => options.MapFrom(MapearProductotallacreacioncolor));
            

            CreateMap<productos, ProductotallaDTO>()
              .ForMember(x => x.DetalleDTO, options => options.MapFrom(MapearProductotalla));

        }


        private List<DetalleDTO> MapearProductotalla(productos producto, ProductotallaDTO productotallaDTO)
        {
            var resultado = new List<DetalleDTO>();
            

            if (producto.productotalla != null)
            {
                foreach (var detalle in producto.productotalla)
                {
                    resultado.Add(new() { Id = detalle.tallaId, cantidad = detalle.cantidad,Nombre=detalle.talla.Nombre });
                }
            }
            if (producto.productocolor != null)
            {
                foreach (var detalle in producto.productocolor)
                {
                    resultado.Add(new() { Id = detalle.ColorId,  cantidad = detalle.cantidad, Nombre = detalle.color.Nombre });
                }
            }

            return resultado;
        }

        // maper de la funcion crear productostalla
        private List<productotalla> MapearProductotallacreaciontalla( ProductoCreacionDTO productoCreacionDTO, productos producto)
        {
            var resultado = new List<productotalla>();
           

            if (productoCreacionDTO.colortallacreacionlistadoDTO == null) { return resultado; }
            
               

                foreach (var talla in productoCreacionDTO.colortallacreacionlistadoDTO)
                {
                    resultado.Add(new productotalla() { tallaId = talla.tallaId ,cantidad=talla.cantidad});
                }

                return resultado;
            

          
        }

        // maper de la funcion crear productoscolor
        private List<productocolor> MapearProductotallacreacioncolor(ProductoCreacionDTO productoCreacionDTO, productos producto)
        {
            var resultado = new List<productocolor>();


            if (productoCreacionDTO.colortallacreacionlistadoDTO == null) { return resultado; }



            foreach (var color in productoCreacionDTO.colortallacreacionlistadoDTO)
            {
                resultado.Add(new productocolor() { ColorId = color.colorId, cantidad=color.cantidad });
            }

            return resultado;



        }
        








        // CreateMap<CineCreacionDTO, Cine>()
        //  .ForMember(x => x.Ubicacion, x => x.MapFrom(dto =>
        //  geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

        // CreateMap<Cine, CineDTO>()
        //   .ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
        //   .ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X));

    }
}

