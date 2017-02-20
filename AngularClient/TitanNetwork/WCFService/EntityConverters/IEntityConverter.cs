using System.Collections.Generic;

namespace BusinessLogicTier.EntityConverters
{
    interface IEntityConverter <TBusinessEntity, TDataTransferEntity>
    {
        TBusinessEntity ToBusinessEntity(TDataTransferEntity model);
        TDataTransferEntity ToDataTransferEntity(TBusinessEntity model);

        IList<TBusinessEntity> ToBusinessEntityList(IList<TDataTransferEntity> models);
        IList<TDataTransferEntity> ToDataTransferEntityList(IList<TBusinessEntity> models);
    }
}
