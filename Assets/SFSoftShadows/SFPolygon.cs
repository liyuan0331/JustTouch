﻿// Super Fast Soft Lighting. Copyright 2015 Howling Moon Software, LLP

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFPolygon : MonoBehaviour {

	private Transform _t;
	public Matrix4x4 _GetMatrix(){
		if (!_t) _t = this.transform;
		return _t.localToWorldMatrix;
	}

	private Rect _bounds;
	public void _UpdateBounds(){
		float l, b, r, t;

		var v0 = _verts[0];
		l = r = v0.x;
		b = t = v0.y;

		for(var i = 1; i < _verts.Length; i++){
			var v = _verts[i];
			l = Mathf.Min(v.x, l);
			r = Mathf.Max(v.x, r);
			b = Mathf.Min(v.y, b);
			t = Mathf.Max(v.y, t);
		}

		_bounds = Rect.MinMaxRect(l, b, r, t);
	}

	private Rect _worldBounds;
	public Rect _WorldBounds{ get {return _worldBounds;}}

	public void _CacheWorldBounds(){
		if (!_t) _t = this.transform;
		_worldBounds = SFRenderer._TransformRect(_t.localToWorldMatrix, _bounds);
	}

	[SerializeField]
	private Vector2[] _verts = new Vector2[3];

	public bool _haveShadow = true;
	public bool _looped;
	public LayerMask _shadowLayers = ~0;
	public float _lightPenetration = 0.0f;
	public float _opacity = 1.0f;
	
	public Vector2[] verts {
		get {
			return _verts;
		}
		
		set {
			_verts = value;
			_UpdateBounds();
		}
	}

	public bool haveShadow {get {return _haveShadow;} set {_haveShadow = value;}}
	public bool looped {get {return _looped;} set {_looped = value;}}
	public LayerMask shadowLayers {get {return _shadowLayers;} set {_shadowLayers = value;}}
	public float lightPenetration {get {return _lightPenetration;} set {_lightPenetration = value;}}
	public float opacity {get {return _opacity;} set {_opacity = value;}}

	public static List<SFPolygon> _polygons = new List<SFPolygon>();
	private void OnEnable(){_polygons.Add(this);}
	private void OnDisable(){_polygons.Remove(this);}

	private void Start(){
		_UpdateBounds();
	}

	public void _CopyVertsFromCollider()
	{
		PolygonCollider2D pc = GetComponent<PolygonCollider2D>();
		if(pc){
			this.looped = true;

			var offsetPoints = pc.points;
			for(int i = 0; i < offsetPoints.Length; i++){
				offsetPoints[i] = offsetPoints[i] + pc.offset;
			}
			this.verts = offsetPoints;

			_FlipInsideOut();
		} else {
			// No collider information. Create a box so it does something.
			this.looped = true;
			this.verts = new Vector2[] {
				new Vector2( 1.0f,  1.0f),
				new Vector2( 1.0f, -1.0f),
				new Vector2(-1.0f, -1.0f),
				new Vector2(-1.0f,  1.0f)
			};
		}
	}

	public void _FlipInsideOut(){
		System.Array.Reverse(verts);
	}
}
