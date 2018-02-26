using ProtoBuf;

//using SuperSDKForUnity3D;
//using SuperSDKForUnity3D.Class;
using sy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitService
{
	private static InitService ins = null;

	public static InitService GetInstance()
	{
		if (ins == null)
		{
			ins = new InitService();
			ins.init();
		}

		return ins;
	}

	private void init()
	{
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_ERROR_CODE, NotifyErrorCode);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_CARRIER_INFO, NotifyCarrierInfo);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_ITEM, loadItemResponse);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_PLAYER, loadPlayerResponse);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_MAIL, ResponseGetMail);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_SHIP, loadShipResponse);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_TACTIC, loadTacticResponse);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_COPY, loadCopyResponse);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_COPY_INFO, copyInfoNotify);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_ITEM_INFO, UpdateItemNotify);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_EQUIP_INFO, UpdateEquipNotify);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_HERO_INFO, UpdateShip);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_RESEARCH_HERO, GetResearchHero);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_HERO_RESEARCH_INFO, LoadHeroResearchInfo);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_CURRENT_CARRIER_INFO, UpdateCurrentCarrierInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_EQUIP_PLANE, SaveCurrentCarrierEquipsInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_MONEY_INFO, UpdateMoneyInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_OIL_INFO, UpdateOilInfo);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_CLEAR_CD, ClearResearchCD);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_HEART_BEAT, ResponseHeartBeat);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_FIGHT_REPORT, ResponseFightReport);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_FIGTH_RESULT, FightResultInfoNotify);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_CARRIER_OPEN_SLOT, ResponseOpenCarrierCell);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_FLOP, ResponseFlopNotify);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_SET_TACTIC, OnSetTacticResponse);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_PLANE_LEVEL_UP, ResponsePlaneLevelUp);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_TEST, ResGMResult);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_RESEARCH_HERO_LOG, ResponseBuildLogs);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_COMPOSE_EQUIP, ResponseCompoundResult);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_SELL_ITEM, ResponseSellItemResult);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_SHOP, ResShopInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_BUY_ITEM, ResBuyItemResule);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_SHOP_INFO, notyfyShopInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_REFRESH_FEATS, ResRefreshShopResult);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_SEND_MAIL, ResponseSendMail);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_MAIL_INFO, NotifyNewMail);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_MY_PK_RANK_INFO, ResPkInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_PK_RANK_LIST, ResPkRankInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_OTHER_PLAYER, ResOtherPlayerInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_TEST_PK_TARGET, ResPkPlayerInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_BUY_COUNT, ResPkBuyTimes);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_BUY_COUNT, NotifyAlreadyBuyTimes);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_PK_PERFORMANCE, NotifyPkPerformance);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_START_PATROL, ResponseStartPatrol);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_PATROL_AWARDS, ResponseGetPatrolAwards);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_PATROL_LEVEL_UP, ResponsePatrolLevelUp);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_FRIEND, ResponseLoadFriendInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_FRIEND_INFO, NotiftFriendInfo);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_UID_BY_NAME, NotiftGetFriendIdByName);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_TOWER_STATE, NotifyTowerState);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_TOWER_AWARD, ResTowerReward);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_TOWER_BUY_BUFF, ResTowerBuyBuff);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_TOWER_BUY_BOX, ResTowerBuyBox);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_COPY_SWEEP, ResCopySweep);
		////NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_BUY_COUNT, OnBuyCountHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_DSTRIKE_SHARE, OnDstrikeShare);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_DSTRIKE_LIST, OnDstrikeListHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_DSTRIKE_FIGHT, OnDstrikeFightHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_RANK_LIST, OnDstrikeRankHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_DSTRIKE_INFO, OnDstrikeSyncInfoHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_DSTRIKE_FIGHT_RESULT, OnDstrikeFightResultHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_DSTRIKE_DAILY_AWARD, OnDstrikeDailyAwardHandle);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_COPY_AWARD, OnGetCopy_Awards);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_COMPOSE_NAVY, ResponseComposeNavy);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_ROB_OPPONENT, ResponseGetRobOpponent);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_USE_TRUCE, ResponseOpenDefense);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_SIGN_IN, ResponseSignIn);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_USE_ITEM, ResponseUseItem);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_TALK, OnNotifyTalkResponse);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_DAILY_AWARD, OnResGetDailyAward);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_ACHIEVEMENT_COUNT, OnNotifyAchievementCount);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_GET_ACHIEVEMENT, OnResGetAchieveAward);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_RANK_INFO, OnUpdateRankResponse);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_EQUIP_LEVEL_UP, OnEquipLevelUpResponse);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_ACTIVE_CARRIER, OnResponseActiveCarrier);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_CHANGE_CARRIER, OnResponseChangeCarrier);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_EQUIP_PLANE_NEW, ResponseEquipPlaneNew);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_PLANE_LEVEL_UP_NEW, ResponsePlaneLevelUpNew);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_BAN_TO_LOGIN, OnNotifyBanLogin);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_RELOGIN, OnNotifyRelogin);

		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_LOAD_PLAYER_END, OnResEnterMainScene);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_NOTIFY_NEW_REPORT, NotifyReport);
		//NetworkManager.GetInstance().AddHandle((int)MSG_CS.MSG_CS_RESPONSE_ACTIVE_RELATION, OnActiveRelationResponse);
	}

	/*
	 * 初始化游戏公共数据
	 * 玩家信息
	 * 军舰信息
	 * 航母信息
	 */

	//public void InitGameData()
	//{
	//	NetworkManager.GetInstance().SendMessage((ushort)MSG_CS.MSG_CS_REQUEST_LOAD_PLAYER, null);
	//	ResquestHeartBeat();
	//}

	//private void ResponsePlaneLevelUpNew(MemoryStream ms)
	//{
	//	Singleton<PlaneModel>.GetInstance().OnLevelUpSuccess();
	//}

	//private void ResponseEquipPlaneNew(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().OnEquipPlaneSuccess();
	//}

	//private void OnResponseChangeCarrier(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().OnChangeCarrier();
	//}

	//private void OnResponseActiveCarrier(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().OnAcitiveCarrier();
	//}

	//private void OnUpdateRankResponse(MemoryStream ms)
	//{
	//	MessageResponseGetRankInfo info = Serializer.Deserialize<MessageResponseGetRankInfo>(ms);
	//	Singleton<MilitaryRankModel>.GetInstance().OnRankUpdateSuccess(info.rank_id);
	//}

	//private void OnEquipLevelUpResponse(MemoryStream ms)
	//{
	//	Singleton<EquipModel>.GetInstance().EquipLevelUpSuccess(ms);
	//}

	//private void OnNotifyTalkResponse(MemoryStream ms)
	//{
	//	MessageNotifyTalk response = Serializer.Deserialize<MessageNotifyTalk>(ms);
	//	if (response != null)
	//	{
	//		if (response.channel != (int)TalkChannel.TALK_CHANNEL_PRIVATE || response.channel == (int)TalkChannel.TALK_CHANNEL_PRIVATE)
	//		{
	//			Singleton<ChatModel>.GetInstance().ProcessChatMsg(response);
	//		}
	//	}
	//}

	//private void ResponseUseItem(MemoryStream ms)
	//{
	//	MessageResponseUseItem info = Serializer.Deserialize<MessageResponseUseItem>(ms);
	//	Singleton<ItemModel>.GetInstance().UseItem(info.results);
	//}

	//private void ResponseSignIn(MemoryStream ms)
	//{
	//	MessageResponseSignIn info = Serializer.Deserialize<MessageResponseSignIn>(ms);
	//	Singleton<ActivityModel>.GetInstance().Check(info.sign_id, info.sign_time);
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchModuleRedEvent(moduleType.eActive);
	//}

	//private void ResponseOpenDefense(MemoryStream ms)
	//{
	//	MessageResponseUseTruce info = Serializer.Deserialize<MessageResponseUseTruce>(ms);
	//	Singleton<RobModel>.GetInstance().OpenDefense(info.truce_until_time);
	//}

	//private void ResponseGetRobOpponent(MemoryStream ms)
	//{
	//	MessageResponseGetRobOpponent info = Serializer.Deserialize<MessageResponseGetRobOpponent>(ms);

	//	Singleton<RobModel>.GetInstance().SetRobPlayerInfo(info.other_info);
	//}

	//private void ResponseComposeNavy(MemoryStream ms)
	//{
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("合成成功");
	//	Singleton<RobModel>.GetInstance().ComposeSucces();
	//}

	//private void ResponsePatrolLevelUp(MemoryStream ms)
	//{
	//	MessageResponsePatrolLevelUp info = Serializer.Deserialize<MessageResponsePatrolLevelUp>(ms);
	//	Singleton<HookModel>.GetInstance().UpdatePatrolInfo(info.patrol_info);
	//}

	//private void ResponseGetPatrolAwards(MemoryStream ms)
	//{
	//	MessageResponseGetPatrolAwards info = Serializer.Deserialize<MessageResponseGetPatrolAwards>(ms);
	//	Singleton<HookModel>.GetInstance().GetReward(info.total_time);
	//}

	//private void ResponseStartPatrol(MemoryStream ms)
	//{
	//	MessageRequestStartPatrol info = Serializer.Deserialize<MessageRequestStartPatrol>(ms);
	//	Singleton<HookModel>.GetInstance().StartPatrol(info.patrol_info);
	//}

	//private void NotifyNewMail(MemoryStream ms)
	//{
	//	MessageNotifyMailInfo info = Serializer.Deserialize<MessageNotifyMailInfo>(ms);

	//	Singleton<MailModel>.GetInstance().UpdateMail(info.new_mail);
	//}

	//private void ResponseSendMail(MemoryStream ms)
	//{
	//	Singleton<MailModel>.GetInstance().SendMail();
	//}

	//private void ResponseGetMail(MemoryStream ms)
	//{
	//	MessageResponseGetMail info = Serializer.Deserialize<MessageResponseGetMail>(ms);
	//	Singleton<MailModel>.GetInstance().UpdateMails(info);
	//	Singleton<MailModel>.GetInstance().DisPatchResGetMail();
	//}

	//private void NotifyCarrierInfo(MemoryStream ms)
	//{
	//	MessageNotifyCarrierInfo info = Serializer.Deserialize<MessageNotifyCarrierInfo>(ms);
	//	Singleton<CarrierModel>.GetInstance().UpdateCarrier(info);
	//}

	//private void ResponseBuildLogs(MemoryStream ms)
	//{
	//	MessageResponseGetResearchHeroLog info = Serializer.Deserialize<MessageResponseGetResearchHeroLog>(ms);
	//	Singleton<BuildShipModel>.GetInstance().SetBuildLogs(info.records);
	//}

	//private void ResponseCarrierOpenSlot(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().OpenSlot();
	//}

	//private void OnSetTacticResponse(MemoryStream ms)
	//{
	//	MessageResponseSetTactic tactic = Serializer.Deserialize<MessageResponseSetTactic>(ms);
	//	Singleton<EmbattleModel>.GetInstance().setEmbattleInfo(tactic.info);
	//}

	//private void ResponsePlaneLevelUp(MemoryStream ms)
	//{
	//	MessageResponsePlaneLevelUp info = Serializer.Deserialize<MessageResponsePlaneLevelUp>(ms);
	//	Singleton<PlaneModel>.GetInstance().PlaneLevelUp(info.new_plane_uid);
	//}

	//private void ResponseOpenCarrierCell(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().OpenCarrierCell();
	//}

	//private void ResquestHeartBeat()
	//{
	//	MessageRequestHeartBeat request = new MessageRequestHeartBeat();
	//	request.millisec = (ulong)Util.DateTimeToStamp(DateTime.Now);
	//	//        MemoryStream stream = new MemoryStream();
	//	//        Serializer.Serialize<MessageRequestHeartBeat>(stream, request);
	//	//        NetworkManager.GetInstance().SendMessage((ushort)MSG_CS.MSG_CS_REQUEST_HEART_BEAT, stream);
	//	NetworkManager.GetInstance().SendMessage<MessageRequestHeartBeat>(MSG_CS.MSG_CS_REQUEST_HEART_BEAT, request);
	//}

	//private void ResponseFightReport(MemoryStream ms)
	//{
	//	MessageResponseFightReport planesInfo = Serializer.Deserialize<MessageResponseFightReport>(ms);
	//	Singleton<FModel>.GetInstance().setFightRecord(planesInfo.info);
	//}

	//private void ResponseHeartBeat(MemoryStream ms)
	//{
	//	MessageResponseHeartBeat info = Serializer.Deserialize<MessageResponseHeartBeat>(ms);
	//	Singleton<GameModel>.GetInstance().UpdateHeartBeat(info);
	//}

	//private void ClearResearchCD(MemoryStream ms)
	//{
	//	MessageResponseClearCD info = Serializer.Deserialize<MessageResponseClearCD>(ms);

	//	if (info.cd_type == (int)CDType.CD_TYPE_RESEARCH_HERO)
	//	{
	//		System.DateTime dt = Util.StampToDateTime(Singleton<BuildShipModel>.GetInstance().GetResearchLastTime());
	//	}
	//	else if (info.cd_type == (int)CDType.CD_TYPE_ARENA)
	//	{
	//		Singleton<PkModel>.GetInstance().ResPkClearCd();
	//	}
	//}

	//private void UpdatePlane(MemoryStream ms)
	//{
	//	//MessageNotifyPlaneInfo planeInfos = Serializer.Deserialize<MessageNotifyPlaneInfo>(ms);
	//	//Singleton<PlaneModel>.GetInstance().UpdatePlanes(planeInfos);
	//}

	//private void GetResearchPlane(MemoryStream ms)
	//{
	//	//MessageResponseGetResearchPlane planesInfo = Serializer.Deserialize<MessageResponseGetResearchPlane>(ms);
	//	//Singleton<BuildPlaneModel>.GetInstance().SetPlanes(planesInfo.plane_uid);
	//}

	//private void LoadPlaneResearchInfo(MemoryStream ms)
	//{
	//	//PlaneResearchInfo info = Serializer.Deserialize<MessageNotifyPlaneResearchInfo>(ms).info;
	//	//Singleton<BuildPlaneModel>.GetInstance().SetResearchInfo(info);
	//}

	//private void UpdateMoneyInfo(MemoryStream ms)
	//{
	//	MessageNotifyMoneyInfo mi = Serializer.Deserialize<MessageNotifyMoneyInfo>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().SetMoneyInfo(mi);
	//}

	//private void UpdateOilInfo(MemoryStream ms)
	//{
	//	MessageNotifyOilInfo mi = Serializer.Deserialize<MessageNotifyOilInfo>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().SetOilInfo(mi);
	//}

	//private void SaveCurrentCarrierEquipsInfo(MemoryStream ms)
	//{
	//	Singleton<CarrierModel>.GetInstance().SaveCarrierPlaneEquipsInfo();
	//}

	//private void UpdateCurrentCarrierInfo(MemoryStream ms)
	//{
	//	MessageNotifyCurrentCarrierInfo ci = Serializer.Deserialize<MessageNotifyCurrentCarrierInfo>(ms);
	//	Singleton<CarrierModel>.GetInstance().setCurrentCarrierInfo(ci.info);
	//}

	//private void NotifyErrorCode(MemoryStream ms)
	//{
	//	MessageErrorCode errorInfo = Serializer.Deserialize<MessageErrorCode>(ms);

	//	if (errorInfo.msg_id == (int)(MSG_CS.MSG_CS_REQUEST_LOAD_PLAYER))
	//	{
	//		//角色不存在需要创建角色
	//		if ((int)errorInfo.err_code == (int)(ResultID.ERR_PLAYER_NEED_CREATE))
	//		{
	//			Debuger.Log("-----------進入劇情模式---------");
	//			Singleton<ModuleEventDispatcher>.GetInstance().dispatchEnterMovieEvent();

	//			Debuger.Log("-----------玩家角色不存在 需要创建角色---------");
	//			Singleton<ModuleEventDispatcher>.GetInstance().dispatchPlayerNeedCreateEvent();
	//		}
	//	}
	//	else
	//	{
	//		//错误提示
	//		string prompt = ConfigDataManager.GetLanguageByKey("NET_ERROR_" + (int)errorInfo.err_code);
	//		Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptWnd(DialogType.OK, prompt);
	//	}
	//}

	//private void UpdateItemNotify(MemoryStream ms)
	//{
	//	MessageNotifyItemInfo itemInfos = Serializer.Deserialize<MessageNotifyItemInfo>(ms);
	//	Singleton<ItemModel>.GetInstance().UpdateItemList(itemInfos);
	//	List<Item> planes = Singleton<ItemModel>.GetInstance().getItemListByType(ItemModel.ItemType.ITEM_TYPE_PLANE);
	//	Singleton<PlaneModel>.GetInstance().setPlaneList(planes);
	//}

	//private void UpdateEquipNotify(MemoryStream ms)
	//{
	//	MessageNotifyEquipInfo equipInfos = Serializer.Deserialize<MessageNotifyEquipInfo>(ms);
	//	Singleton<EquipModel>.GetInstance().UpdateEquipList(equipInfos);
	//}

	//private void UpdateShip(MemoryStream ms)
	//{
	//	MessageNotifyHeroInfo heroInfos = Serializer.Deserialize<MessageNotifyHeroInfo>(ms);
	//	Singleton<ShipModel>.GetInstance().UpdateShips(heroInfos);
	//}

	//private void GetResearchHero(MemoryStream ms)
	//{
	//	MessageResponseGetResearchHero hero = Serializer.Deserialize<MessageResponseGetResearchHero>(ms);
	//	Singleton<BuildShipModel>.GetInstance().SetShip(hero);
	//}

	//private void LoadHeroResearchInfo(MemoryStream ms)
	//{
	//	HeroResearchInfo info = Serializer.Deserialize<MessageNotifyHeroResearchInfo>(ms).info;
	//	Singleton<BuildShipModel>.GetInstance().SetBuildTime(info);
	//}

	//private void loadItemResponse(MemoryStream ms)
	//{
	//	MessageResponseLoadItem item = Serializer.Deserialize<MessageResponseLoadItem>(ms);
	//	Singleton<ItemModel>.GetInstance().setData(item.items);
	//	Singleton<EquipModel>.GetInstance().setData(item.equips);
	//	List<Item> planes = Singleton<ItemModel>.GetInstance().getItemListByType(ItemModel.ItemType.ITEM_TYPE_PLANE);
	//	Singleton<PlaneModel>.GetInstance().setPlaneList(planes);
	//}

	//private void loadTacticResponse(MemoryStream ms)
	//{
	//	MessageResponseLoadTactic tactic = Serializer.Deserialize<MessageResponseLoadTactic>(ms);
	//	Singleton<EmbattleModel>.GetInstance().setEmbattleInfo(tactic.info);

	//	HeroInfo shipInfo = Singleton<ShipModel>.GetInstance().getCurrentShip();
	//	if (shipInfo == null)
	//	{
	//		for (int i = 1; i <= 6; i++)
	//		{
	//			PositionInfo info = Singleton<EmbattleModel>.GetInstance().getPosInfo(i);
	//			if (info != null)
	//			{
	//				shipInfo = Singleton<ShipModel>.GetInstance().GetByUID(info.hero_uid);
	//				if (shipInfo != null)
	//				{
	//					Singleton<ShipModel>.GetInstance().setShowHeroInfo(shipInfo);
	//					break;
	//				}
	//			}
	//		}
	//	}
	//}

	//private void loadShipResponse(MemoryStream ms)
	//{
	//	MessageResponseLoadShip ship = NetworkManager.GetInstance().Deserialize<MessageResponseLoadShip>(ms);
	//	//        MessageResponseLoadShip ship = Serializer.Deserialize<MessageResponseLoadShip>(ms);
	//	Singleton<ShipModel>.GetInstance().setShipList(ship.ships);
	//	Singleton<CarrierModel>.GetInstance().setCarrierList(ship.carrier);
	//	Singleton<CarrierModel>.GetInstance().setCurrentCarrierInfo(ship.current_carrier);
	//	//Singleton<CarrierModel>.GetInstance().setCarrierTechInfo(ship.carrier_tech);
	//	Singleton<BuildShipModel>.GetInstance().SetBuildTime(ship.hero_research);
	//	//Singleton<BuildPlaneModel>.GetInstance().SetResearchInfo(ship.plane_research);
	//}

	//private void loadPlayerResponse(MemoryStream ms)
	//{
	//	MessageResponseLoadPlayer player = Serializer.Deserialize<MessageResponseLoadPlayer>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().SetPlayerInfo(player);
	//	Singleton<HookModel>.GetInstance().SetPatrolInfos(player.patrol_info, player.patrol_total_time);
	//	Singleton<RobModel>.GetInstance().OpenDefense(player.info.truce_time);
	//	//		SuperSDKOpenApi.GetInstance().OnEnterGame(GameData.GetInstance());
	//}

	//private void loadCopyResponse(MemoryStream ms)
	//{
	//	MessageResponseLoadCopy copy = Serializer.Deserialize<MessageResponseLoadCopy>(ms);
	//	Singleton<CopyModel>.GetInstance().SetCopyProgress(copy.progress);
	//	Singleton<CopyModel>.GetInstance().SetCopyStarList(copy.copy_star);
	//	Singleton<CopyModel>.GetInstance().SetPassedCopy(copy.passed_copy);
	//	Singleton<CopyModel>.GetInstance().SetCopyCounts(copy.copy_count);
	//	Singleton<CopyModel>.GetInstance().SetChapterAwards(copy.chapter_award);
	//	Singleton<CopyModel>.GetInstance().SetGateAwards(copy.gate_award);
	//}

	//private void copyInfoNotify(MemoryStream ms)
	//{
	//	MessageNotifyCopyInfo copy = Serializer.Deserialize<MessageNotifyCopyInfo>(ms);
	//	Singleton<CopyModel>.GetInstance().UpdateCopyProgress(copy.progress);
	//	Singleton<CopyModel>.GetInstance().UpdateCopyStarList(copy.copy_star);
	//	Singleton<CopyModel>.GetInstance().UpdatePassedCopy(copy.passed_copy);
	//	Singleton<CopyModel>.GetInstance().UpdateCopyCounts(copy.copy_count);
	//}

	//private void FightResultInfoNotify(MemoryStream ms)
	//{
	//	Debuger.Log("~~~~~~~FightResultInfoNotify");
	//	MessageNotifyFightResult fightresule = Serializer.Deserialize<MessageNotifyFightResult>(ms);
	//	FightResultInfoModel.FightResultType type = Singleton<FightResultInfoModel>.GetInstance().CurFightResultType;
	//	if (type == FightResultInfoModel.FightResultType.eCopyFight)
	//	{
	//		Singleton<FightResultInfoModel>.GetInstance().ResFightResult(fightresule);
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eTowerSweep)
	//	{
	//		Singleton<ChallengeModel>.GetInstance().AddTowerSweepInfo(fightresule);
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eCopySweep)
	//	{
	//		Singleton<CopyMupopModel>.GetInstance().ResponseMupopHandle(fightresule);
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eRobSweep)
	//	{
	//		Singleton<RobModel>.GetInstance().ResponseMopUpHandle(fightresule);
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eDailyFight)
	//	{
	//		Singleton<DailyCopyModel>.GetInstance().IsLastDailyCopy = true;
	//		Singleton<FightResultInfoModel>.GetInstance().ResFightResult(fightresule);
	//	}
	//}

	//private void ResponseFlopNotify(MemoryStream ms)
	//{
	//	MessageResponseFlop info = Serializer.Deserialize<MessageResponseFlop>(ms);
	//	Singleton<FightResultInfoModel>.GetInstance().setDrawIndexList(info.index);
	//	Singleton<FightResultInfoModel>.GetInstance().setdrawBagId(info.flop_id);
	//	Singleton<RobModel>.GetInstance().SetFlopResult(info);
	//}

	//private void ResGMResult(MemoryStream ms)
	//{
	//	Debuger.Log("----------GM返回成功-------------");
	//}

	////合成 和 商店
	//private void ResponseCompoundResult(MemoryStream ms)
	//{
	//	MessageResponseComposeEquip info = Serializer.Deserialize<MessageResponseComposeEquip>(ms);
	//	if (info.item_uid != null && info.item_uid.Count > 0)
	//	{
	//		Singleton<CompoundModel>.GetInstance().dispathRefreshCompoundModule();
	//		List<Item> itemlist = new List<Item>();
	//		Item iteminfo = Singleton<ItemModel>.GetInstance().Clone(info.item_uid[0]);
	//		iteminfo.count = info.item_uid.Count;
	//		itemlist.Add(iteminfo);
	//		Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptItemsWnd(itemlist);
	//	}
	//	if (info.ship_uid != null && info.ship_uid.Count > 0)
	//	{
	//		Singleton<CompoundModel>.GetInstance().dispathRefreshCompoundModule();
	//		HeroInfo heroInfo = Singleton<ShipModel>.GetInstance().GetByUID(info.ship_uid[0]);
	//		if (heroInfo != null)
	//		{
	//			ShipEffectModule shipEffectModule = ModuleManager.GetInstance().CreateModule("shipeffect") as ShipEffectModule;
	//			shipEffectModule.SetShipInfo(heroInfo.hero_id, info.ship_uid.Count);
	//		}
	//	}
	//}

	//private void ResponseSellItemResult(MemoryStream ms)
	//{
	//	MessageResponseSellItem info = Serializer.Deserialize<MessageResponseSellItem>(ms);
	//	if (info != null)
	//	{
	//		Singleton<CompoundModel>.GetInstance().dispathRefreshCompoundModule();
	//		Singleton<ItemModel>.GetInstance().DispatchItemSellSuccess();
	//		string prompttext = "";
	//		List<sy.MoneyType> moneytype = info.money;
	//		for (int i = 0; i < moneytype.Count; i++)
	//		{
	//			prompttext = TypeConst.RewardTypeStr[((WinGradeModule.RewardType)moneytype[i].kind).ToString()] + moneytype[i].value;
	//			if (prompttext.Length > 0)
	//				Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("出售成功，恭喜获得" + prompttext);
	//		}
	//	}
	//}

	//private void ResShopInfo(MemoryStream ms)
	//{
	//	MessageResponseLoadShop info = Serializer.Deserialize<MessageResponseLoadShop>(ms);
	//	Singleton<ShopModel>.GetInstance().UpdataShopInfo(info);
	//}

	//private void ResBuyItemResule(MemoryStream ms)
	//{
	//	int commodity_id = Singleton<ShopModel>.GetInstance().buy_commodity_id;
	//	ShopConfigInfo configInfo = ConfigDataManager.getConfigInfoById("shop", commodity_id) as ShopConfigInfo;
	//	if (configInfo != null)
	//	{
	//		string[] itemId = configInfo.item.Split('|');
	//		if (itemId.Length > 1)
	//		{
	//			List<sy.Item> itemlist = new List<Item>();
	//			sy.Item item = new Item();
	//			item.item_id = int.Parse(itemId[0]);
	//			item.count = int.Parse(itemId[1]) * Singleton<ShopModel>.GetInstance().buy_commodity_count;
	//			itemlist.Add(item);
	//			Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptItemsWnd(itemlist);
	//		}
	//	}
	//	else
	//		Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("购买成功");
	//	Singleton<ShopModel>.GetInstance().SuccessBuy();
	//	//Singleton<ShopModel>.GetInstance().dispatchBuyItemsSucceed();
	//}

	//private void notyfyShopInfo(MemoryStream ms)
	//{
	//	MessageNotifyShopInfo info = Serializer.Deserialize<MessageNotifyShopInfo>(ms);
	//	Singleton<ShopModel>.GetInstance().UpdataRefreshShopInfo(info);
	//}

	//private void ResRefreshShopResult(MemoryStream ms)
	//{
	//	Singleton<ShopModel>.GetInstance().dispatchRefreshShopInfo();
	//}

	//private void ResPkInfo(MemoryStream ms)
	//{
	//	MessageResponseGetMyPkRankInfo info = Serializer.Deserialize<MessageResponseGetMyPkRankInfo>(ms);
	//	Singleton<PkModel>.GetInstance().ResPkInfo(info);
	//}

	//private void ResPkRankInfo(MemoryStream ms)
	//{
	//	MessageResponseGetPkRankList info = Serializer.Deserialize<MessageResponseGetPkRankList>(ms);
	//	Singleton<PkModel>.GetInstance().ResPkRankInfo(info);
	//}

	//private void ResOtherPlayerInfo(MemoryStream ms)
	//{
	//	MessageResponseGetOtherPlayer info = Serializer.Deserialize<MessageResponseGetOtherPlayer>(ms);
	//	if (Singleton<RobModel>.GetInstance().IsSendMsg)
	//	{
	//		Singleton<RobModel>.GetInstance().ResponseOhterPlayerInfos(info.infos);
	//	}
	//	else if (Singleton<CommonModel>.GetInstance().IsViewOtherPlayer && info.infos.Count > 0)
	//	{
	//		Singleton<CommonModel>.GetInstance().ResponseOtherPlayerInfo(info.infos[0]);
	//	}
	//	else
	//	{
	//		Singleton<OtherPlayerInfoModel>.GetInstance().ResOtherPlayerInfo(info);
	//	}
	//}

	//private void ResPkPlayerInfo(MemoryStream ms)
	//{
	//	MessageResponseTestPkTarget info = Serializer.Deserialize<MessageResponseTestPkTarget>(ms);
	//	PKRankInfo rankInfo = new PKRankInfo();
	//	rankInfo.player_id = info.player_id;
	//	rankInfo.rank = info.rank;
	//	Singleton<PkModel>.GetInstance().dispatchCheckPkPlayerInfo(rankInfo);
	//}

	//private void NotifyAlreadyBuyTimes(MemoryStream ms)
	//{
	//	MessageNotifyBuyCount info = Serializer.Deserialize<MessageNotifyBuyCount>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().ResSetAlreadyBuyTimes(info);
	//}

	//private void ResPkBuyTimes(MemoryStream ms)
	//{
	//	MessageResponseBuyCount info = Serializer.Deserialize<MessageResponseBuyCount>(ms);
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("购买成功");
	//	if (info.count_type == (int)CountType.COUNT_TYPE_ARENA)
	//	{
	//		Singleton<PkModel>.GetInstance().dispacthResBuyPkTimes();
	//	}
	//	else if (info.count_type == (int)CountType.COUNT_TYPE_ELITE_COPY)
	//	{
	//		Singleton<ChallengeModel>.GetInstance().dispacthResBuyEliteTimes();
	//	}
	//	else
	//	{
	//		Singleton<CopyModel>.GetInstance().ResponesBuyCount(info.count_type);
	//	}
	//}

	//public void NotifyPkPerformance(MemoryStream ms)
	//{
	//	MessageNotifyPkPerformance info = Serializer.Deserialize<MessageNotifyPkPerformance>(ms);
	//	Singleton<PkModel>.GetInstance().setPkPerformance(info);
	//}

	//public void NotifyReport(MemoryStream ms)
	//{
	//	MessageNotifyNewReport info = Serializer.Deserialize<MessageNotifyNewReport>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().AddReport(info.report_abstract);
	//}

	//private void ResponseLoadFriendInfo(MemoryStream stream)
	//{
	//	MessageResponseLoadFriend info = Serializer.Deserialize<MessageResponseLoadFriend>(stream);
	//	Singleton<FriendModel>.GetInstance().SyncFriendData(info.infos);
	//}

	//private void NotiftFriendInfo(MemoryStream stream)
	//{
	//	MessageNotifyFriendInfo friendInfo = Serializer.Deserialize<MessageNotifyFriendInfo>(stream);
	//	Singleton<FriendModel>.GetInstance().SyncSimpleFriendInfo(friendInfo);
	//}

	//private void NotiftGetFriendIdByName(MemoryStream stream)
	//{
	//	MessageResponseGetUIDByName uidByName = Serializer.Deserialize<MessageResponseGetUIDByName>(stream);
	//	Singleton<FriendModel>.GetInstance().GetFriendIdByNameHandle(uidByName);
	//}

	//public void NotifyTowerState(MemoryStream ms)
	//{
	//	MessageNotifyTowerState info = Serializer.Deserialize<MessageNotifyTowerState>(ms);
	//	Singleton<PlayerInfoModel>.GetInstance().SetTowerState(info);
	//}

	//public void ResTowerReward(MemoryStream ms)
	//{
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptText("领取成功");
	//	Singleton<ChallengeModel>.GetInstance().dispatchResTowerReward();
	//}

	//public void ResTowerBuyBuff(MemoryStream ms)
	//{
	//	Singleton<ChallengeModel>.GetInstance().dispatchResTowerBuyBuff();
	//}

	//public void ResTowerBuyBox(MemoryStream ms)
	//{
	//	MessageResponseTowerBuyBox info = Serializer.Deserialize<MessageResponseTowerBuyBox>(ms);
	//	Singleton<ChallengeModel>.GetInstance().ResBuyTowerBox(info);
	//}

	//public void ResCopySweep(MemoryStream ms)
	//{
	//	MessageResponseCopySweep info = Serializer.Deserialize<MessageResponseCopySweep>(ms);
	//	FightResultInfoModel.FightResultType type = Singleton<FightResultInfoModel>.GetInstance().CurFightResultType;
	//	if (type == FightResultInfoModel.FightResultType.eTowerSweep)
	//	{
	//		Singleton<ChallengeModel>.GetInstance().AddResCopyIdList(info.copy_id);
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eCopyFight)
	//	{
	//	}
	//	else if (type == FightResultInfoModel.FightResultType.eRobSweep)
	//	{
	//		Singleton<RobModel>.GetInstance().ArmyPartUpdate();
	//	}
	//}

	//private void OnDstrikeSyncInfoHandle(MemoryStream stream)
	//{
	//	MessageNotifyDstrikeInfo loadPlayer = Serializer.Deserialize<MessageNotifyDstrikeInfo>(stream);
	//	Singleton<DstrikeModel>.GetInstance().SyncDstrikeInfo(loadPlayer.info);
	//}

	//private void OnDstrikeShare(MemoryStream stream)
	//{
	//	MessageNotifyDstrikeShare share = Serializer.Deserialize<MessageNotifyDstrikeShare>(stream);
	//	Singleton<DstrikeModel>.GetInstance().SyncDstrikeShare(share);
	//}

	//private void OnDstrikeRankHandle(MemoryStream stream)
	//{
	//	MessageResponseGetRankList rankList = Serializer.Deserialize<MessageResponseGetRankList>(stream);
	//	RankType type = (RankType)rankList.rank_type;
	//	switch (type)
	//	{
	//		//主线副本星数排行
	//		case RankType.RANK_TYPE_STAR_1:

	//			break;
	//		//精英副本星数排行
	//		case RankType.RANK_TYPE_STAR_2:

	//			break;
	//		//爬塔星数排行
	//		case RankType.RANK_TYPE_TOWER:
	//			Singleton<ChallengeModel>.GetInstance().ResTowerRank(rankList.list);
	//			break;
	//		//围剿叛军功勋排行
	//		case RankType.RANK_TYPE_DSTRIKE_EXPLOIT:
	//			Singleton<DstrikeModel>.GetInstance().SyncDstrikeGongXunRank(rankList.list);
	//			break;
	//		//围剿叛军伤害排行
	//		case RankType.RANK_TYPE_DSTRIKE_DAMAGE:
	//			Singleton<DstrikeModel>.GetInstance().SyncDstrikeDmgRank(rankList.list);
	//			break;
	//		//等级排行榜
	//		case RankType.RANK_TYPE_LEVEL:
	//			Singleton<RankModel>.GetInstance().ResMainRank(type, rankList.list);
	//			break;
	//		//战斗力排行榜
	//		case RankType.RANK_TYPE_FIGHT:
	//			Singleton<RankModel>.GetInstance().ResMainRank(type, rankList.list);
	//			break;
	//	}
	//}

	//private void OnDstrikeFightHandle(MemoryStream stream)
	//{
	//	MessageResponseDstrikeFight fight = Serializer.Deserialize<MessageResponseDstrikeFight>(stream);
	//	Singleton<DstrikeModel>.GetInstance().SyncDstrikeFight(fight);
	//}

	//private void OnDstrikeListHandle(MemoryStream stream)
	//{
	//	MessageResponseDstrikeList list = Serializer.Deserialize<MessageResponseDstrikeList>(stream);
	//	Singleton<DstrikeModel>.GetInstance().SyncDstrikeList(list);
	//}

	//private void OnDstrikeFightResultHandle(MemoryStream stream)
	//{
	//	MessageNotifyDstrikeBossFightResult result = Serializer.Deserialize<MessageNotifyDstrikeBossFightResult>(stream);
	//	Singleton<DstrikeModel>.GetInstance().SyncNotifyDstrikeBossFightResult(result);
	//}

	//private void OnGetCopy_Awards(MemoryStream stream)
	//{
	//	Singleton<CopyModel>.GetInstance().SetGateAwards(stream);
	//}

	////
	//private void OnDstrikeDailyAwardHandle(MemoryStream stream)
	//{
	//	MessageErrorCode awards = new MessageErrorCode();
	//	awards.err_code = ResultID.ERR_OK;
	//	Singleton<DstrikeModel>.GetInstance().SyncDstrikeAwards(awards);
	//}

	////日常领取返回
	//private void OnResGetDailyAward(MemoryStream stream)
	//{
	//	Singleton<DailyTaskModel>.GetInstance().dispatchRefreshDailyTask();
	//	Singleton<DailyTaskModel>.GetInstance().ShowRewardItem();
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchModuleRedEvent(moduleType.eTask);
	//}

	////日常领取返回
	//private void OnResGetAchieveAward(MemoryStream stream)
	//{
	//	Singleton<RobModel>.GetInstance().CollcetAward();
	//	Singleton<DailyTaskModel>.GetInstance().dispatchRefreshDailyTask();
	//	Singleton<DailyTaskModel>.GetInstance().ShowRewardItem();
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchModuleRedEvent(moduleType.eTask);
	//}

	//private void OnNotifyAchievementCount(MemoryStream stream)
	//{
	//	MessageNotifyAchievement ms = Serializer.Deserialize<MessageNotifyAchievement>(stream);
	//	Singleton<PlayerInfoModel>.GetInstance().SetAchievementCount(ms.achievements);
	//}

	////private void OnBuyCountHandle(MemoryStream stream)
	////{
	////    MessageResponseBuyCount buyCount = Serializer.Deserialize<MessageResponseBuyCount>(stream);
	////    Singleton<CopyModel>.GetInstance().ResponesBuyCount(buyCount.count_type);
	////}

	//private void OnNotifyBanLogin(MemoryStream stream)
	//{
	//}

	//private void OnNotifyRelogin(MemoryStream stream)
	//{
	//	Singleton<GameModel>.GetInstance().isReLogin = true;
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchPromptWnd(DialogType.OK, "您的账号在其他地方登陆，您已被迫下线");
	//}

	//private void OnResEnterMainScene(MemoryStream stream)
	//{
	//	Singleton<ModuleEventDispatcher>.GetInstance().dispatchEnterMainscene();
	//}

	////盟约激活刷新界面
	//private void OnActiveRelationResponse(MemoryStream ms)
	//{
	//	Singleton<ShipModel>.GetInstance().dispatchEventResActiveRelation();
	//}
}