using UnityEngine;
using System.Collections;

public enum GameState {
	PrePlay,
	Spawning,
	WaitingForWaveCleared,
	Transition,
	Suspended,
	Over
}
