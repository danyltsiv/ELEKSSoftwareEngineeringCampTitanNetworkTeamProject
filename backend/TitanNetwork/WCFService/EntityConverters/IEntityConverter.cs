using System.Collections.Generic;

namespace WCFService.EntityConverters
{
    /// <summary>
    /// Converts entities from WCF layer to Database Layer
    /// </summary>
    /// <typeparam name="TBusinessEntity">The type of the t business entity.</typeparam>
    /// <typeparam name="TDataTransferEntity">The type of the t data transfer entity.</typeparam>
    interface IEntityConverter <TBusinessEntity, TDataTransferEntity>
    {
        /// <summary>
        /// To the business entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TBusinessEntity.</returns>
        TBusinessEntity ToBusinessEntity(TDataTransferEntity model);
        /// <summary>
        /// To the data transfer entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TDataTransferEntity.</returns>
        TDataTransferEntity ToDataTransferEntity(TBusinessEntity model);

        /// <summary>
        /// To the business entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TBusinessEntity&gt;.</returns>
        IList<TBusinessEntity> ToBusinessEntityList(IList<TDataTransferEntity> models);
        /// <summary>
        /// To the data transfer entity list.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>IList&lt;TDataTransferEntity&gt;.</returns>
        IList<TDataTransferEntity> ToDataTransferEntityList(IList<TBusinessEntity> models);
    }
}
