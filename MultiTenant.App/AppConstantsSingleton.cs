namespace MultiTenant.App;

internal sealed class AppConstantsSingleton
{
	private const string DEFAULT_TENANT_IDENTIFIER = "tenantCode";
	private static AppConstantsSingleton _instance;

	internal readonly string TenantIdentifier;

	private AppConstantsSingleton(IConfiguration configuration)
	{
		TenantIdentifier = configuration[nameof(TenantIdentifier)] ?? DEFAULT_TENANT_IDENTIFIER;
	}

	internal static void Init(IConfiguration configuration)
	{
		if (_instance == null)
			_instance = new AppConstantsSingleton(configuration);
	}

	internal static AppConstantsSingleton Instance => _instance;
}
