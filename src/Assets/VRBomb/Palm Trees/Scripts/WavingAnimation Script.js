﻿#pragma strict

function Start () {

}

function Update () {
	var waving : Vector4 = renderer.material.GetVector("_WaveAndDistance");
	var wavingPower : float = waving.w;
	
	waving.x += (Time.deltaTime*0.1*wavingPower*Random.value);
	waving.y -= (Time.deltaTime*0.1*wavingPower*Random.value );
	
	renderer.material.SetVector("_WaveAndDistance", Vector4(waving.x,waving.y,waving.z,waving.w));
}