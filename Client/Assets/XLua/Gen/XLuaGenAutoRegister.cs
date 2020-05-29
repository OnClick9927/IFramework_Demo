#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(IFramework.Lua.Pedding), IFrameworkLuaPeddingWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(IFramework.Lua.MyStruct), IFrameworkLuaMyStructWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(IFramework.Lua.MyEnum), IFrameworkLuaMyEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(IFramework.Lua.NoGc), IFrameworkLuaNoGcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.WaitForSeconds), UnityEngineWaitForSecondsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(IFramework.Lua.BaseTest), IFrameworkLuaBaseTestWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(IFramework.Lua.IExchanger), IFrameworkLuaIExchangerBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(IFramework.Lua.Luac.ICalc), IFrameworkLuaLuacICalcBridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE0( UnityEngine.Transform target,  UnityEngine.Vector3 end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE1( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE2( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE3( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE4( UnityEngine.Transform target,  UnityEngine.Vector3 end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE5( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE6( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE7( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE8( UnityEngine.Transform target,  UnityEngine.Vector3 end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE9( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE10( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Vector3> __GEN_DELEGATE11( UnityEngine.Transform target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Quaternion> __GEN_DELEGATE12( UnityEngine.Transform target,  UnityEngine.Quaternion end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Quaternion> __GEN_DELEGATE13( UnityEngine.Transform target,  UnityEngine.Vector3 end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Quaternion> __GEN_DELEGATE14( UnityEngine.Transform target,  UnityEngine.Quaternion end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Quaternion> __GEN_DELEGATE15( UnityEngine.Transform target,  UnityEngine.Vector3 end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Color> __GEN_DELEGATE16( UnityEngine.Material target,  UnityEngine.Color end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Color> __GEN_DELEGATE17( UnityEngine.Light target,  UnityEngine.Color end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Color> __GEN_DELEGATE18( UnityEngine.Camera target,  UnityEngine.Color end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<UnityEngine.Color> __GEN_DELEGATE19( UnityEngine.Material target,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<int> __GEN_DELEGATE20( UnityEngine.UI.Text target,  int start,  int end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<float> __GEN_DELEGATE21( UnityEngine.UI.Text target,  float start,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<string> __GEN_DELEGATE22( UnityEngine.UI.Text target,  string start,  string end,  float dur,  IFramework.EnvironmentType env);
		
		delegate IFramework.Tweens.Tween<float> __GEN_DELEGATE23( UnityEngine.UI.Image target,  float start,  float end,  float dur,  IFramework.EnvironmentType env);
		
		delegate bool __GEN_DELEGATE24( UnityEngine.Object o);
		
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
				{typeof(UnityEngine.Transform), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE0(IFramework.Tweens.TweenEx.DoMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE1(IFramework.Tweens.TweenEx.DoMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE2(IFramework.Tweens.TweenEx.DoMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE3(IFramework.Tweens.TweenEx.DoMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE4(IFramework.Tweens.TweenEx.DoLocalMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE5(IFramework.Tweens.TweenEx.DoLocalMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE6(IFramework.Tweens.TweenEx.DoLocalMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE7(IFramework.Tweens.TweenEx.DoLocalMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE8(IFramework.Tweens.TweenEx.DoScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE9(IFramework.Tweens.TweenEx.DoScaleX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE10(IFramework.Tweens.TweenEx.DoScaleY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE11(IFramework.Tweens.TweenEx.DoScaleZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE12(IFramework.Tweens.TweenEx.DoRota)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE13(IFramework.Tweens.TweenEx.DoRota)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE14(IFramework.Tweens.TweenEx.DoLocalRota)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE15(IFramework.Tweens.TweenEx.DoLocalRota)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Material), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE16(IFramework.Tweens.TweenEx.DoColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE19(IFramework.Tweens.TweenEx.DoAlpha)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Light), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE17(IFramework.Tweens.TweenEx.DoColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Camera), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE18(IFramework.Tweens.TweenEx.DoColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Text), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE20(IFramework.Tweens.TweenEx.DoText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE21(IFramework.Tweens.TweenEx.DoText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE22(IFramework.Tweens.TweenEx.DoText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.UI.Image), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE23(IFramework.Tweens.TweenEx.DoFillAmount)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Object), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE24(IFramework.Lua.UnityEngineObjectEx.IsNull)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
