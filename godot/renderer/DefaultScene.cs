using System;
using System.Reflection;
using System.Threading;
using Godot;

public partial class DefaultScene : Node3D {

	public static DefaultScene GlobalInstance;
	
	public static Assembly Genesis;
	public static Thread GenesisThread;

	public DefaultScene() {
		Genesis = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "/GENESIS.dll");
	}

	public override void _Ready() {
		base._Ready();
		
		GlobalInstance = this;

		var program = Genesis.GetType("GENESIS.Program");
		var programMain = program.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);

		GenesisThread = new Thread(() => {
			programMain.Invoke(null, [new[] { "--debug" }]);
		});
		GenesisThread.Start();
	}

	public void Clear() {
		foreach(var child in GetChildren()) {
			RemoveChild(child);
			child.QueueFree();
		}
	}
}
