using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Controller : MonoBehaviour {

  [SerializeField]
  private float pullRadius = 500;
  [SerializeField]
  private float pullForce = 1;
  [SerializeField]
  private float growRate = 20;

  public void FixedUpdate() {
    pullRadius = pullRadius + growRate * Time.fixedDeltaTime;
    foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
      Vector3 forceDirection = transform.position - collider.transform.position;
      collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
    }
  }



}
