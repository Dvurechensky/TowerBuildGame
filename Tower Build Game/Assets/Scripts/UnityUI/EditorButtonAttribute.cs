using System;

namespace CustomAttributes
{
    /// <summary>
    /// Нажатие на кнопку в редакторе
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonAttribute : Attribute
    {
        /// <summary>
        /// Button text
        /// </summary>
        public string name;

        /// <summary>
        /// Add Button to Inspector
        /// </summary>
        /// <param name="name">Button text</param>
        public EditorButtonAttribute(string name)
        {
            this.name = name;
        }
    }
}
