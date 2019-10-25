using System.ComponentModel;
using OSharp.Core.Packs;
using OSharp.Entity;

namespace Liuliu.Demo.Systems.Dtos
{
    /// <summary>
    /// ���DTO��ģ�����Ϣ
    /// </summary>
    public class PackOutputDto : IOutputDto
    {
        /// <summary>
        /// ��ȡ������ ����
        /// </summary>
        [DisplayName("����")]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ��ʾ����
        /// </summary>
        [DisplayName("��ʾ����")]
        public string Display { get; set; }

        /// <summary>
        /// ��ȡ������ ����·��
        /// </summary>
        [DisplayName("����·��")]
        public string Class { get; set; }

        /// <summary>
        /// ��ȡ������ ģ�鼶��
        /// </summary>
        [DisplayName("����")]
        public PackLevel Level { get; set; }

        /// <summary>
        /// ��ȡ������ ����˳��
        /// </summary>
        [DisplayName("����˳��")]
        public int Order { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�����
        /// </summary>
        [DisplayName("�Ƿ�����")]
        public bool IsEnabled { get; set; }
    }
}