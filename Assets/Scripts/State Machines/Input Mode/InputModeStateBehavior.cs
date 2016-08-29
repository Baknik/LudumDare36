using UnityEngine;
using System.Collections;

public class InputModeStateBehavior : StateMachineBehaviour {

	public delegate void InputModeEnteredDelegate(InputMode inputMode);
	public static event InputModeEnteredDelegate InputModeEntered;

	public delegate void InputModeExitedDelegate(InputMode inputMode);
	public static event InputModeExitedDelegate InputModeExited;

	public delegate void InputModeStayDelegate(InputMode inputMode);
	public static event InputModeStayDelegate InputModeStay;

	[Header("Configuration Fields")]
	public InputMode Mode;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		InputModeEntered(this.Mode);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		InputModeStay(this.Mode);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		InputModeExited(this.Mode);
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
