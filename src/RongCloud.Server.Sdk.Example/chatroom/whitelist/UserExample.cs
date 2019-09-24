﻿using System;
using System.Threading.Tasks;
using RongCloud.Server.methods.chatroom.whitelist;
using RongCloud.Server.models.chatroom;
using RongCloud.Server.models.response;

namespace RongCloud.Server.Sdk.Example.chatroom.whitelist
{
    public class UserExample

    {
        /**
         * 此处替换成您的appKey
         * */
        private static readonly string appKey = "kj7swf8okyqt2";

        /**
         * 此处替换成您的appSecret
         * */
        private static readonly string appSecret = "mFe3U1UClx4gx";

        /**
         * 自定义api地址
         * */
        private static readonly string api = "http://api.cn.ronghub.com";

        static async Task main(string[] args)
        {
            RongCloud rongCloud = RongCloud.GetInstance(appKey, appSecret);
            //自定义 api地址方式
            //RongCloud rongCloud = RongCloud.getInstance(appKey, appSecret,api);

            Whitelist whitelist = rongCloud.Chatroom.whiteList;

            /**
             * API: 文档http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/user.html#add
             * 添加聊天室用户白名单
             * */
            ChatroomMember[] members =
            {
                new ChatroomMember() {Id = "qawr34h"}, new ChatroomMember() {Id = "qawr35h"}
            };
            ChatroomModel chatroom = new ChatroomModel()
            {
                Id = "d7ec7a8b8d8546c98b0973417209a548",
                Members = members
            };

            ResponseResult addResult = await whitelist.User.Add(chatroom);
            Console.WriteLine("add whitelist:  " + addResult);

            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/user.html#getList
             * 获取聊天室用户白名单
             * */

            WhiteListResult getResult = await whitelist.User.GetList(chatroom);
            Console.WriteLine("get whitelist:  " + getResult);


            /**
             * API 文档: http://www.rongcloud.cn/docs/server_sdk_api/chatroom/whitelist/user.html#remove
             * 删除聊天室用户白名单
             * */

            ResponseResult removeResult = await whitelist.User.Remove(chatroom);
            Console.WriteLine("remove whitelist:  " + removeResult);

            Console.ReadLine();
        }
    }
}