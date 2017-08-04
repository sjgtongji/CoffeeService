using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public abstract class MapperManager<PO, DTO> where PO : POBase, new() where DTO : DTOBase, new()
    {
        //protected MapperManager() {
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMissingTypeMaps = true;
        //        cfg.CreateMap<PO, DTO>().ReverseMap();
        //    });
        //}

        public PO DtoToPo(DTO dto)
        {
            if (dto == null)
                return null;

            PO po = Mapper.Map<DTO, PO>(dto);
            return po;
        }

        public List<PO> DtoToPoList(List<DTO> listDto)
        {
            List<PO> listPo = new List<PO>();
            if (listDto == null || listDto.Count == 0)
                return listPo;

            listDto.ForEach(x => {
                listPo.Add(DtoToPo(x));
            });

            return listPo;
        }

        public DTO PoToDto(PO po)
        {
            if (po == null)
                return null;

            DTO dto = Mapper.Map<PO, DTO>(po);
            return dto;
        }

        public List<DTO> PoToDtoList(List<PO> listPo)
        {
            List<DTO> listDto = new List<DTO>();
            if (listPo == null || listPo.Count == 0)
                return listDto;

            listPo.ForEach(x => {
                listDto.Add(PoToDto(x));
            });

            return listDto;
        }

    }
}
