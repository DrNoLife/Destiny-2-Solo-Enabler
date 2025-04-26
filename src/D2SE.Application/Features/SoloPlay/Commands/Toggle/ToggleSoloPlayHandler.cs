using D2SE.Application.Features.SoloPlay.Commands.Broadcast;
using D2SE.Domain.Enums;
using D2SE.Domain.Entities;
using D2SE.Domain.Interfaces.Infrastructure;
using MediatR;
using System.Diagnostics;

namespace D2SE.Application.Features.SoloPlay.Commands.Toggle;

public class ToggleSoloPlayHandler(IFirewallService firewallService, ISender mediator, ISettingsService settingsService, IAlertService alertService) : IRequestHandler<ToggleSoloPlayCommand>
{
    private readonly IFirewallService _firewallService = firewallService;
    private readonly ISettingsService _settingsService = settingsService;
    private readonly ISender _mediator = mediator;
    private readonly IAlertService _alertService = alertService;

    public async Task Handle(ToggleSoloPlayCommand request, CancellationToken cancellationToken)
    {
        var rulesActive = _firewallService.FirewallRulesExists();

        Debug.WriteLine($"Rules currently active: {rulesActive}");

        if (rulesActive)
        {
            _firewallService.RemoveFirewallRules();
            Debug.WriteLine("Want to remove rules");
            _alertService.ShowAlert("Solo play disabled");
        }
        else
        {
            FirewallRule ruleEntity = RetrievePortRangeToBlock();

            _firewallService.CreateFirewallRules(ruleEntity);
            Debug.WriteLine("Want to add rules");
            _alertService.ShowAlert("Solo play enabled", $"Blocking ports: {ruleEntity.PortValue}");
        }


        await _mediator.Send(new BroadcastSoloPlayStatusCommand(), cancellationToken);
    }

    /// <summary>
    /// Retrieves a <see cref="FirewallRule"/> whose <c>PortValue</c> is set according to the following priority:
    /// <list type="number">
    ///   <item>
    ///     If a port range was specified via command-line arguments (e.g. “-PortRange 3000-4000”), that value is used.
    ///   </item>
    ///   <item>
    ///     Otherwise, if the user has saved a custom port range in settings (<see cref="SettingsNames.CustomPortRange"/>), that is used.
    ///   </item>
    ///   <item>
    ///     If neither is provided, the default port range baked into <see cref="FirewallRule.CreateRule"/> (via <see cref="D2SEConstants.PortRange"/>) remains.
    ///   </item>
    /// </list>
    /// </summary>
    /// <returns>
    /// A <see cref="FirewallRule"/> instance with its <c>PortValue</c> set to the resolved port range to block.
    /// </returns>
    private FirewallRule RetrievePortRangeToBlock()
    {
        FirewallRule ruleEntity = FirewallRule.CreateRule();

        // See if the user has specified custom port range using the program itself.
        var userHasChangedCustomPortSetting = _settingsService.CheckIfSettingExists(SettingsNames.OverridePortRange);
        var userOverwrotePortRange = _settingsService.GetSettingsValue<bool>(SettingsNames.OverridePortRange);
        if (userHasChangedCustomPortSetting && userOverwrotePortRange)
        {
            var customPortRange = _settingsService.GetSettingsValue<string>(SettingsNames.CustomPortRange);
            if (!String.IsNullOrEmpty(customPortRange))
            {
                ruleEntity = ruleEntity with { PortValue = customPortRange };
            }
        }
        
        // See if we can get the port range from command line arguments.
        var portRangeFromCommandLineArguments = GetPortRangeFromCommandLineArguments();
        if (!String.IsNullOrEmpty(portRangeFromCommandLineArguments))
        {
            ruleEntity = ruleEntity with { PortValue = portRangeFromCommandLineArguments };
        }

        return ruleEntity;
    }

    private static string GetPortRangeFromCommandLineArguments()
    {
        var commandLineArgs = Environment.GetCommandLineArgs().ToList();
        var portRangeIndex = commandLineArgs.IndexOf("-PortRange");

        return portRangeIndex != -1
            ? commandLineArgs[portRangeIndex + 1]
            : String.Empty;
    }
}
