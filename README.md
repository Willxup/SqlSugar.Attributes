# 🏗️ SqlSugar.Attributes

[![SqlSugar.Attributes](https://img.shields.io/nuget/v/SqlSugar.Attributes)](https://www.nuget.org/packages/SqlSugar.Attributes)

`SqlSugar.Attributes`基于[`SqlSugar`](https://github.com/DotNetNext/SqlSugar)和`C#`特性 配置dto模型，只需要在模型上配置表信息，即可自动拼接SQL，实现查询插入更新等功能，实现高效项目开发和迭代。

# 🚀Getting Started

## ⚓ 查询

使用`SqlSugar.Attributes`可以直接配置DTO模型，配置完成后，将会自动映射查询条件及结果。

- 创建查询的查询条件类

```c#
[DbDefaultOrderBy("t_Name", DbSortWay.DESC)]
//[DbDefaultOrderBy("t.t_Name", DbSortWay.DESC)]
public class Search
{
	//[DbTableAlias("t")] //表别名，多表关联使用，单表需要查询时指定表别名
	[DbQueryField("t_Name")] //字段名
	[DbQueryOperator(DbOperator.Like)]  //操作符, Like查询
	public string Name { get; set; }
	//[DbTableAlias("t")] //表别名，多表关联使用，单表需要查询时指定表别名
	[DbQueryField("t_CodeId")] //字段名
	[DbQueryOperator(DbOperator.In)] //操作符，IN查询
	public List<long> CodeIds { get; set; }
}
```

- 创建查询的结果类

```c#
public class Result
{
	//[DbTableAlias("t")] //表别名，多表关联使用，单表需要查询时指定表别名
	[DbQueryField("t_Id")] //字段名
	public long Id { get; set; }
	//[DbTableAlias("t")] //表别名，多表关联使用，单表需要查询时指定表别名
	[DbQueryField("t_Name")] //字段名
	public string Name { get; set; }
    //子查询，如果指定了表别名，可以使用表别名，否则直接写表名
	//[DbSubQuery("(SELECT name FROM Table_Name2 WHERE t.t_Id = Id)")]
    [DbSubQuery("(SELECT name FROM Table_Name2 WHERE Table_Name.t_Id = Id)")]
    public string OtherName { get; set; }
}
```



- 查询方法

```c#
public class Test
{
    /// <summary>
    /// SqlSugar dbcontext
    /// </summary>
	private readonly ISqlSugarClient _dbContext;
    
    public async Task<List<Result>> Get(Search search)
    {
    	return await _dbContext.Queryable<Table_Name>()
            .Where(search) //查询条件
            .OrderBy(search) //排序
            .Select(new Result()) //查询结果
            .ToListAsync();
    }
}
```

注：在使用`SqlSugar.Attribute`时，依然可以使用`SqlSugar`的`Where`，`GroupBy`，`OrderBy`等方法。



- 正常查询SQL

```sql
SELECT 
t_Id as Id, 
t_Name as Name, 
(SELECT name FROM Table_Name2 WHERE Table_Name.t_Id = Id) AS OtherName
FROM Table_Name
WHERE t_Name LIKE '%@Name%' AND t_CodeId IN (@CodeIds)
ORDER BY t_Name DESC;
```



- 指定表别名查询SQL

```sql
SELECT 
t.t_Id as Id, 
t.t_Name as Name,
(SELECT name FROM Table_Name2 WHERE t.t_Id = Id) AS OtherName
FROM Table_Name t
WHERE t.t_Name LIKE '%@Name%' AND t.t_CodeId IN (@CodeIds)
ORDER BY t.t_Name DESC;
```



## ⚓ 插入

- 配置DTO模型

```c#
public class AddModel
{
	[DbOperationField("t_Name")]
	public string Name { get; set; }
	
	[DbOperationField("t_Address")]
	public string Address { get; set; }
}
```



- 新增方法

```c#
public class Test
{
    /// <summary>
    /// SqlSugar dbcontext
    /// </summary>
	private readonly ISqlSugarClient _dbContext;
    
    public async Task<int> Add(AddModel model)
    {
    	return await _dbContext.Insertable<AddModel,Table_Name>(model)
            .ExecuteCommandAsync();
    }
}
```

注：`ExecuteCommandAsync()`方法为`SqlSugar`的方法，这意味着可以使用其他的`SqlSugar`方法。



## ⚓ 更新

- 配置DTO模型

```c#
public class EditModel
{
	[DbOperationField("t_Id", true)] //true代表更新条件
	public long? Id { get; set; }
	
	[DbOperationField("t_Name")]
	public string Name { get; set; }
	
	[DbOperationField("t_Address")]
	public string Address { get; set; }
}
```



- 更新方法

```c#
public class Test
{
    /// <summary>
    /// SqlSugar dbcontext
    /// </summary>
	private readonly ISqlSugarClient _dbContext;
    
    public async Task<bool> Add(EditModel model)
    {
    	return await _dbContext.Updateable<EditModel,Table_Name>(model)
            .ExecuteCommandHasChangeAsync();
    }
}
```

注：`ExecuteCommandHasChangeAsync()`方法为`SqlSugar`的方法，这意味着可以使用其他的`SqlSugar`方法。



## ⚓ 更多用法

更多用法请参阅[FastAdminAPI](https://github.com/Willxup/FastAdminAPI)项目，该项目对`SqlSugar.Attributes`进行了更多的封装，更易于上手。
