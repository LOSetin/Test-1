using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrow.Framework.CodeFactory
{
    public interface IFactory
    {
        #region Sql语句
        string SqlAllFields(List<FieldInfo> fields);

        string SqlInsert(string tableName, List<FieldInfo> fields);

        string SqlUpdate(string tableName, List<FieldInfo> fields);

        string SqlDelete(string tableName);

        string SqlSelect(string tableName, List<FieldInfo> fields);

        string SqlTop(string tableName, List<FieldInfo> fields);

        string SqlPage(string tableName, List<FieldInfo> fields);

        string SqlCount(string tableName);

        #endregion

        #region 基本方法
        /// <summary>
        /// 名字格式化方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string NameFormat(string name);

        /// <summary>
        /// 参数符号，例如@，: 等
        /// </summary>
        string ParaSymbol { get; }

        /// <summary>
        /// 扩展字段名
        /// </summary>
        string ExtraFieldName { get; }

        /// <summary>
        /// 获得所有表
        /// </summary>
        /// <returns></returns>
        List<string> GetTables();

        /// <summary>
        /// 获得所有视图
        /// </summary>
        /// <returns></returns>
        List<string> GetViews();

        /// <summary>
        /// 获得表所有字段
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <returns></returns>
        List<FieldInfo> GetTableFields(string tbName);

        /// <summary>
        /// 获得视图所有字段
        /// </summary>
        /// <param name="viewName">视图名</param>
        /// <returns></returns>
        List<FieldInfo> GetViewFields(string viewName);

        /// <summary>
        /// 获取该表所有具有唯一约束的字段名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<string> GetUniqueFields(string tableName);

        /// <summary>
        /// 获取该表所有外键，不支持复合键
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<ForeignKeyInfo> GetForeignKeys(string tableName);

        /// <summary>
        /// 数据库类型转换为C#类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        string ToCSType(string type, int length, int scale);

        /// <summary>
        /// 数据库类型转换为类似dr.GetString(0)中的String的类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        string ToReaderGetType(string type, int length, int scale);
        #endregion

    }
}
