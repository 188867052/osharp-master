using System;
using Liuliu.Demo.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OSharp.Entity;

namespace Liuliu.Demo.EntityConfiguration.Security
{
    public class EntityUserConfiguration : EntityTypeConfigurationBase<EntityUser, Guid>
    {
        /// <summary>
        /// ��д��ʵ��ʵ�����͸������Ե����ݿ�����
        /// </summary>
        /// <param name="builder">ʵ�����ʹ�����</param>
        public override void Configure(EntityTypeBuilder<EntityUser> builder)
        {
            builder.HasIndex(m => new { m.EntityId, m.UserId }).HasName("EntityUserIndex");

            builder.HasOne(eu => eu.EntityInfo).WithMany().HasForeignKey(m => m.EntityId);
            builder.HasOne(eu => eu.User).WithMany().HasForeignKey(m => m.UserId);
        }
    }
}