using System;

namespace DDD.Domain.Core.Models
{
    public abstract class EntityAudit : Entity
    {
        public DateTime Criacao { get; set; }
        public long CriadoPorId { get; set; }
        public DateTime? Alteracao { get; set; }
        public long? AlteradoPorId { get; set; }
        public DateTime? Exclusao { get; set; }
        public long? ExcluidoPorId { get; set; }
        public void SetEntityAuditValues(EntityAudit entityAudit)
        {
            if (entityAudit == null)
                return;

            Criacao = entityAudit.Criacao;
            CriadoPorId = entityAudit.CriadoPorId;
            Alteracao = entityAudit.Alteracao;
            AlteradoPorId = entityAudit.AlteradoPorId;
            Exclusao = entityAudit.Exclusao;
            ExcluidoPorId = entityAudit.ExcluidoPorId;
            Excluido = entityAudit.Excluido;
        }
    }
}
