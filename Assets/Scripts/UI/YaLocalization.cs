using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YaLocalization {
	public enum Language{SimplifiedChinese,English,}
	static Language language=Language.SimplifiedChinese;

	static public void SetLanguage(Language _language){
		language = _language;
	}

	static string[,] ActMsg=new string[,]{
		{"第一幕     初见世界",	"第二幕     其它的圆"},
		{"Act1     New World",	"Act1     ?????"},
	};
	static public string GetActMsg(int index){
		if (index < 1) {
			return "error";
		}
		return ActMsg [(int)language, index-1];
	}

	static string[][,] ActMonoLogue = new string[][,]{ 
		new string[,] {
			{"只有天知道我是怎么变成了一个圆\n也只有天知道我是怎么来到了这个奇怪的世界",		"这种情况下我自然的开始考虑苏格拉底的三大终极哲学问题:我是谁、我从哪来、要到哪去,我发现自己的确没找到答案\n我的意思是:我发现自己失忆了",		"于是一直徘徊在这个虚无的世界里"}, 
			{"e1",						"e2",					"e3"},
		},
		new string[,] {
			{"看来我并不孤单，在我的面前的是另一个圆，我也许该好好观察一下它在做什么\n等等，这是什么情况，它变大的同时...我缩小了？",		"我只知道,在这个世界,我无法置身事外"}, 
			{"前面的是刺吗？似乎挺尖利\n我现在的体积恐怕钻不过去,先等等看吧",						"e2"},
		},
	};
	static public string GetLevelMonoLogue(int levelNum, int index){
		if (index < 1 || levelNum<1){
			return "error";
		}
		return ActMonoLogue [levelNum - 1] [(int)language, index - 1];
	}


}
