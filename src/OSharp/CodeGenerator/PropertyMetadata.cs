// -----------------------------------------------------------------------
//  <copyright file="PropertyMetadata.cs" company="OSharp��Դ�Ŷ�">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>������</last-editor>
//  <last-date>2018-08-06 12:31</last-date>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using OSharp.Reflection;

namespace OSharp.CodeGenerator
{
    /// <summary>
    /// ����Ԫ����
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// ��ʼ��һ��<see cref="PropertyMetadata"/>���͵���ʵ��
        /// </summary>
        public PropertyMetadata()
        {
        }

        /// <summary>
        /// ��ʼ��һ��<see cref="PropertyMetadata"/>���͵���ʵ��
        /// </summary>
        public PropertyMetadata(PropertyInfo property)
        {
            if (property == null)
            {
                return;
            }

            this.Name = property.Name;
            this.TypeName = property.PropertyType.FullName;
            this.Display = property.GetDescription();
            RequiredAttribute required = property.GetAttribute<RequiredAttribute>();
            if (required != null)
            {
                this.IsRequired = !required.AllowEmptyStrings;
            }

            StringLengthAttribute stringLength = property.GetAttribute<StringLengthAttribute>();
            if (stringLength != null)
            {
                this.MaxLength = stringLength.MaximumLength;
                this.MinLength = stringLength.MinimumLength;
            }
            else
            {
                this.MaxLength = property.GetAttribute<MaxLengthAttribute>()?.Length;
                this.MinLength = property.GetAttribute<MinLengthAttribute>()?.Length;
            }

            RangeAttribute range = property.GetAttribute<RangeAttribute>();
            if (range != null)
            {
                this.Range = new[] { range.Minimum, range.Maximum };
                this.Max = range.Maximum;
                this.Min = range.Minimum;
            }

            this.IsNullable = property.PropertyType.IsNullableType();
            if (this.IsNullable)
            {
                this.TypeName = property.PropertyType.GetUnNullableType().FullName;
            }

            // ö�����ͣ���Ϊ��ֵ���ͷ���
            if (property.PropertyType.IsEnum)
            {
                Type enumType = property.PropertyType;
                Array values = enumType.GetEnumValues();
                Enum[] enumItems = values.Cast<Enum>().ToArray();
                if (enumItems.Length > 0)
                {
                    this.EnumMetadatas = enumItems.Select(m => new EnumMetadata(m)).ToArray();
                }
            }
        }

        /// <summary>
        /// ��ȡ������ ��������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ������ ������������
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// ��ȡ������ ��ʾ����
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ����
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// ��ȡ������ ��󳤶�
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// ��ȡ������ ��С����
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// ��ȡ������ ȡֵ��Χ
        /// </summary>
        public object[] Range { get; set; }

        /// <summary>
        /// ��ȡ������ ���ֵ
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// ��ȡ������ ��Сֵ
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// ��ȡ������ �Ƿ�ֵ���Ϳɿ�
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// ��ȡ������ ö��Ԫ����
        /// </summary>
        public EnumMetadata[] EnumMetadatas { get; set; }

        /// <summary>
        /// �Ƿ�����֤����
        /// </summary>
        public bool HasValidateAttribute()
        {
            return this.IsRequired.HasValue || this.MaxLength.HasValue || this.MinLength.HasValue || this.Range != null || this.Max != null || this.Min != null;
        }
    }
}