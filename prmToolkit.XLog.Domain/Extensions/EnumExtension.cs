using System;
using System.ComponentModel;

namespace prmToolkit.XLog.Domain.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// Obtém o Enum através do nome passado
        /// </summary>
        /// <typeparam name="T">Tipo genérico que representa o Enum</typeparam>
        /// <param name="value">Nome do Enum</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            
            return (T)System.Enum.Parse(typeof(T), value);

            
        }

        /// <summary>
        /// Obter a descrição do atributo definido no enum
        /// </summary>
        /// <param name="value">Valor do Enum</param>
        /// <returns>String contendo a descrição do Enum</returns>
        public static string GetDescription(this System.Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Obter o atributo customizado do enum
        /// </summary>
        /// <typeparam name="T">Tipo genérico que representa o Enum</typeparam>
        /// <param name="value">Valor do Enum</param>
        /// <returns>String contendo o valor da descrição do atributo</returns>
        private static T GetAttribute<T>(this System.Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        /// <summary>
        /// Validar se o valor que está no Enum foi definido corretamente
        /// </summary>
        /// <param name="value">Valor setado no Enum</param>
        /// <returns>Retorna se o Enum possuí valor válido.</returns>
        public static bool IsEnumValid(this System.Enum value)
        {
            if (System.Enum.IsDefined(value.GetType(), value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validar se o valor pertence a um Enum
        /// </summary>
        /// <typeparam name="TEnum">Tipo genérico que representa o Enum</typeparam>
        /// <param name="enumValue">Valor do inteiro</param>
        /// <returns>Retorna se o valor do inteiro informado, existe dentro do Enum.</returns>
        public static bool IsEnumValid<TEnum>(this int enumValue)
        {
            if (System.Enum.IsDefined(typeof(TEnum), enumValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retorna nome do Enum selecionado
        /// </summary>
        /// <param name="valueEnum">Valor do Enum selecionado pelo usuário</param>
        /// <returns>Retorna o nome do Enum selecionado</returns>
        public static string GetName(this System.Enum valueEnum)
        {
            return valueEnum.ToString();
        }
    }
}
