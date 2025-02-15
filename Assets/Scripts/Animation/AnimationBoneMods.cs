﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public static partial class AnimationManager
{
	static Dictionary<string, string> boneMODName_to_skeleton = new Dictionary<string, string>
	{
		["chin_MOD_Joint_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jaw1_Joint_bind/chin_MOD_Joint_bind",
		["jawCorners_MOD_Joint_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jaw1_Joint_bind/jawCorners_MOD_Joint_bind",
		["jt_lowLipParent_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jaw1_Joint_bind/jawCorners_MOD_Joint_bind/jt_lowLipParent_MOD_bind",
		["jt_nose_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_nose_MOD_bind",
		["jt_noseParent_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_nose_MOD_bind/jt_noseParent_MOD_bind",
		["jt_noseBridge_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_noseBridge_MOD_bind",
		["jt_L_eye_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_L_eye_MOD_bind",
		["jt_L_eyeParent_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_L_eye_MOD_bind/jt_L_eyeParent_MOD_bind",
		["jt_R_eye_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_R_eye_MOD_bind",
		["jt_R_eyeParent_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_R_eye_MOD_bind/jt_R_eyeParent_MOD_bind",
		["jt_mouth_MOD_bind"] = "Armature/jt_all_bind/jt_hips_bind/spine1_loResSpine2_bind/spine1_loResSpine3_bind/head1_neck_bind/jt_head_bind/jt_mouth_MOD_bind",
	};

	static Dictionary<string, Vector3> boneMODDefault_position = new Dictionary<string, Vector3>
	{
		["chin_MOD_Joint_bind"] = new Vector3(1.400244e-16f, -3.916735f, 2.928557f),
		["jawCorners_MOD_Joint_bind"] = new Vector3(0.00242094f, -1.961419f, -0.2943561f),
		["jt_lowLipParent_MOD_bind"] = new Vector3(-1.387779e-16f, 1.961419f, 0.294366f),
		["jt_nose_MOD_bind"] = new Vector3(-0.4869505f, 3.511287e-14f, 4.476315f),
		["jt_noseParent_MOD_bind"] = new Vector3(-3.489662e-14f, -0.4869505f, -4.476315f),
		["jt_noseBridge_MOD_bind"] = new Vector3(-2.376049f, 3.61496e-14f, 4.345019f),
		["jt_L_eye_MOD_bind"] = new Vector3(-2.574324f, -1.844644f, 3.032392f),
		["jt_L_eyeParent_MOD_bind"] = new Vector3(1.844644f, -2.574573f, -3.023612f),
		["jt_R_eye_MOD_bind"] = new Vector3(-2.574324f, 1.844644f, 3.032392f),
		["jt_R_eyeParent_MOD_bind"] = new Vector3(-1.844644f, 2.574573f, 3.023612f),
		["jt_mouth_MOD_bind"] = new Vector3(0.9160487f, -2.145447e-15f, 4.235064f),
	};

	static Dictionary<string, Quaternion> boneMODDefault_rotation = new Dictionary<string, Quaternion>
	{
		["chin_MOD_Joint_bind"] = Quaternion.Euler(new Vector3(0, 0, 0)),
		["jawCorners_MOD_Joint_bind"] = Quaternion.Euler(new Vector3(0, -0.471f, 0)),
		["jt_lowLipParent_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0.471f, 0)),
		["jt_nose_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, 90)),
		["jt_noseParent_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, -90)),
		["jt_noseBridge_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, 90)),
		["jt_L_eye_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, 90)),
		["jt_L_eyeParent_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, -90)), //0, 0, -90
		["jt_R_eye_MOD_bind"] = Quaternion.Euler(new Vector3(0, 180, 90)), //Should be 0, 180, 90
		["jt_R_eyeParent_MOD_bind"] = Quaternion.Euler(new Vector3(0, 180, 90)), //Should be 0, 180, 90
		["jt_mouth_MOD_bind"] = Quaternion.Euler(new Vector3(0, 0, 0)),
	};

	static Dictionary<string, string> bone_fixes = new Dictionary<string, string>
	{
		["jt_all_bind1"] = "jt_all_bind", //Just why
		["jt_all_bind2"] = "jt_all_bind",
		["jt_all_bind3"] = "jt_all_bind",
		["jt_all_bind4"] = "jt_all_bind",
		["jt_all_bind5"] = "jt_all_bind",
	};
	public static void setBoneMODMods(ref AnimationClip anim_clip, ref Dictionary<string, BoneMod> bone_mods)
	{
		foreach (string key in bone_mods.Keys)
		{
			if (key.Contains("MOD"))
			{
				List<Keyframe> key_frames_pos_x = new List<Keyframe>();
				List<Keyframe> key_frames_pos_y = new List<Keyframe>();
				List<Keyframe> key_frames_pos_z = new List<Keyframe>();

				List<Keyframe> key_frames_rot_x = new List<Keyframe>();
				List<Keyframe> key_frames_rot_y = new List<Keyframe>();
				List<Keyframe> key_frames_rot_z = new List<Keyframe>();
				List<Keyframe> key_frames_rot_w = new List<Keyframe>();

				List<Keyframe> key_frames_scale_x = new List<Keyframe>();
				List<Keyframe> key_frames_scale_y = new List<Keyframe>();
				List<Keyframe> key_frames_scale_z = new List<Keyframe>();

				key_frames_pos_x.Add(new Keyframe(0, bone_mods[key].translation[0] + boneMODDefault_position[key].x));
				key_frames_pos_y.Add(new Keyframe(0, bone_mods[key].translation[1] + boneMODDefault_position[key].y));
				key_frames_pos_z.Add(new Keyframe(0, bone_mods[key].translation[2] + boneMODDefault_position[key].z));



				Quaternion resulting_quaternion = bone_mods[key].rotation * boneMODDefault_rotation[key];

				key_frames_rot_x.Add(new Keyframe(0, resulting_quaternion.x));
				key_frames_rot_y.Add(new Keyframe(0, resulting_quaternion.y));
				key_frames_rot_z.Add(new Keyframe(0, resulting_quaternion.z));
				key_frames_rot_w.Add(new Keyframe(0, resulting_quaternion.w));



				key_frames_scale_x.Add(new Keyframe(0, bone_mods[key].scale[0]));
				key_frames_scale_y.Add(new Keyframe(0, bone_mods[key].scale[1]));
				key_frames_scale_z.Add(new Keyframe(0, bone_mods[key].scale[2]));



				string bone_full_name = boneMODName_to_skeleton[key];

				AnimationCurve curve_pos_x = new AnimationCurve(key_frames_pos_x.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localPosition.x", curve_pos_x);

				AnimationCurve curve_pos_y = new AnimationCurve(key_frames_pos_y.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localPosition.y", curve_pos_y);

				AnimationCurve curve_pos_z = new AnimationCurve(key_frames_pos_z.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localPosition.z", curve_pos_z);

				AnimationCurve curve_rot_w = new AnimationCurve(key_frames_rot_w.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localRotation.w", curve_rot_w);

				AnimationCurve curve_rot_y = new AnimationCurve(key_frames_rot_y.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localRotation.y", curve_rot_y);

				AnimationCurve curve_rot_z = new AnimationCurve(key_frames_rot_z.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localRotation.z", curve_rot_z);

				AnimationCurve curve_rot_x = new AnimationCurve(key_frames_rot_x.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localRotation.x", curve_rot_x);

				AnimationCurve curve_scale_x = new AnimationCurve(key_frames_scale_x.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localScale.x", curve_scale_x);

				AnimationCurve curve_scale_y = new AnimationCurve(key_frames_scale_y.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localScale.y", curve_scale_y);

				AnimationCurve curve_scale_z = new AnimationCurve(key_frames_scale_z.ToArray());
				anim_clip.SetCurve(bone_full_name, typeof(Transform), "localScale.z", curve_scale_z);
			}
		}


	}

	public class BoneMod
	{
		public BoneMod(Vector3 _p, Quaternion _r, Vector3 _s)
		{
			translation = _p;
			rotation = _r;
			scale = _s;
			CameraHack = false;
		}


		public bool CameraHack;
		public Vector3 translation;
		public Quaternion rotation;
		public Vector3 scale;
	}



}

