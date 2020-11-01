﻿using UnityEngine;

public class Gravity_Controller : MonoBehaviour {
  
  // Radius to get objects to be pulled it
  [SerializeField]
  private float pullRadius = 500;
  // The force by which the objects will be pulled
  [SerializeField]
  private float pullForce = 1;
  // The rate the radius will grow
  [SerializeField]
  private float growRate = 20;

  public void FixedUpdate() {
    // Increase the radius with every update
    pullRadius = pullRadius + growRate * Time.fixedDeltaTime;
    // Loop through the objects within the radius
    foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
      Vector3 forceDirection = transform.position - collider.transform.position;
      // Apply the force to the object
      if (collider.GetComponent<Rigidbody>() != null)
        collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
    }
  }



}
