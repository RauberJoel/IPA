using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using ScrumRetroApp.Blazor.Services;

public class TrackingCircuitHandler : CircuitHandler
{
	#region Fields
	private readonly HashSet<Circuit> circuits = new HashSet<Circuit>();

	#endregion

	#region Properties
	public int ConnectedCircuits => circuits.Count;
	#endregion

	#region Publics
	public override Task OnConnectionUpAsync(Circuit circuit,
	                                         CancellationToken cancellationToken)
	{
		circuits.Add(circuit);

		return Task.CompletedTask;
	}

	public override Task OnConnectionDownAsync(Circuit circuit,
	                                           CancellationToken cancellationToken)
	{
		circuits.Remove(circuit);

		return Task.CompletedTask;
	}

	public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
	{
		return base.OnCircuitOpenedAsync(circuit, cancellationToken);
	}

	public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
	{
		return base.OnCircuitClosedAsync(circuit, cancellationToken);
	}
	#endregion
}