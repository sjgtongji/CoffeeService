using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMS.Core;
using XMS.Inner.Coffee.Model;

namespace XMS.Inner.Coffee.Business
{
    public class DataMapperManagerBase<PO, DTO> where PO : POBase, new() where DTO : DTOBase, new()
    {
        public PO DtoToPo(DTO dto)
        {
            if (dto == null)
                return null;

            PO po = new PO();
            dto.MemberwiseCopy<DTO>(po);
            return po;
        }

        public List<PO> DtoToPoList(List<DTO> listDto)
        {
            List<PO> listPo = new List<PO>();
            if (listDto == null || listDto.Count == 0)
                return listPo;

            listDto.ForEach(x =>
            {
                listPo.Add(DtoToPo(x));
            });

            return listPo;
        }

        public DTO PoToDto(PO po)
        {
            if (po == null)
                return null;

            DTO dto = new DTO();
            po.MemberwiseCopy<PO>(dto);
            return dto;
        }

        public List<DTO> PoToDtoList(List<PO> listPo)
        {
            List<DTO> listDto = new List<DTO>();
            if (listPo == null || listPo.Count == 0)
                return listDto;

            listPo.ForEach(x =>
            {
                listDto.Add(PoToDto(x));
            });

            return listDto;
        }
    }
}
