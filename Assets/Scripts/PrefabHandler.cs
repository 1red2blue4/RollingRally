using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton which holds all the Prefabs used in the game
public class PrefabHandler : Singleton<PrefabHandler> {
	[SerializeField]
	private GameObject _emptyTile;
	public GameObject EmptyTile
	{
		get { return _emptyTile; }
	}
	[SerializeField]
	private GameObject _ramp45;
	public GameObject Ramp45
	{
		get { return _ramp45; }
	}
	[SerializeField]
	private GameObject _wall45;
	public GameObject Wall45
	{
		get { return _wall45; }
	}
	[SerializeField]
	private GameObject _shortRamp;
	public GameObject ShortRamp
	{
		get { return _shortRamp; }
	}
	[SerializeField]
	private GameObject _tallRamp;
	public GameObject TallRamp
	{
		get { return _tallRamp; }
	}
	[SerializeField]
	private GameObject _fullBlock;
	public GameObject FullBlock
	{
		get { return _fullBlock; }
	}
	[SerializeField]
	private GameObject _halfBlockH;
	public GameObject HalfBlockHorizontal
	{
		get { return _halfBlockH; }
	}
	[SerializeField]
	private GameObject _halfBlockV;
	public GameObject HalfBlockVertical
	{
		get { return _halfBlockV; }
	}
	[SerializeField]
	private GameObject _ball;
	public GameObject Ball
	{
		get { return _ball; }
	}
	[SerializeField]
	private GameObject _canvas;
	public GameObject Canvas
	{
		get { return _canvas; }
	}
	[SerializeField] 
	private GameObject _goal;
	public GameObject Goal
	{
		get { return _goal; }
	}
	[SerializeField]
	private PhysicMaterial _fricLow;
	public PhysicMaterial FrictionLow
	{
		get { return _fricLow; }
	}
	[SerializeField]
	private PhysicMaterial _fricMed;
	public PhysicMaterial FrictionMed
	{
		get { return _fricMed; }
	}
	[SerializeField]
	private PhysicMaterial _fricHigh;
	public PhysicMaterial FrictionHigh
	{
		get { return _fricHigh; }
	}
	[SerializeField]
	private Material _boltedRed;
	public Material BoltedRed
	{
		get { return _boltedRed; }
	}
	[SerializeField]
	private Material _boltedYellow;
	public Material BoltedYellow
	{
		get { return _boltedYellow; }
	}
	[SerializeField]
	private Material _boltedGreen;
	public Material BoltedGreen
	{
		get { return _boltedGreen; }
	}
	[SerializeField]
	private GameObject _failBlocks;
	public GameObject FailBlocks
	{
		get { return _failBlocks; }
	}
}
