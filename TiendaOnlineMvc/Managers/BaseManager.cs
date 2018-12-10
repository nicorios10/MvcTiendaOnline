using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TiendaOnlineMvc.Managers
{
    /// <summary>
    /// Clase con metodos comunes de los managers.
    /// </summary>
    public class BaseManager
    {
        /// <summary>
        /// Retorna si el reader tiene una columna especifica
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}