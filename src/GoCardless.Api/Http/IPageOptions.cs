namespace GoCardlessApi.Http
{
    public interface IPageOptions
    {
        /// <summary>
        /// The id of the item to start paging after.
        /// <para>Given the following payments:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Id: PM000003, CreatedAt: 2020-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000002, CreatedAt: 2019-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000001, CreatedAt: 2018-01-01</term>
        /// </item>
        /// </list>
        /// <code>After = "PM000003"</code>
        /// <para>Returns:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Id: PM000002, CreatedAt: 2019-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000001, CreatedAt: 2018-01-01</term>
        /// </item>
        /// </list>
        /// </summary>
        string After { get; set; }

        /// <summary>
        /// The id of the item to start paging before.
        /// <para>Given the following payments:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Id: PM000003, CreatedAt: 2020-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000002, CreatedAt: 2019-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000001, CreatedAt: 2018-01-01</term>
        /// </item>
        /// </list>
        /// <code>Before = "PM000001"</code>
        /// <para>Returns:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>Id: PM000003, CreatedAt: 2020-01-01</term>
        /// </item>
        /// <item>
        /// <term>Id: PM000002, CreatedAt: 2019-01-01</term>
        /// </item>
        /// </list>
        /// </summary>
        string Before { get; set; }

        /// <summary>
        /// The maximum amount of items to be returned. If not set, all items will be returned.
        /// </summary>
        int? Limit { get; set; }
    }
}