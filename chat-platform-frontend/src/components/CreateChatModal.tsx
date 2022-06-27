import React, {useState} from "react";
import {SignalRService} from "../services/signalRService";
import IChat from "../models/IChat";

interface CreateChatModalProps {
    CloseCallback: () => void;
    SignalRService: SignalRService;
    CreateChatCallback: (chat: IChat) => void;
}

function CreateChatModal(props: CreateChatModalProps) {
    const [chatName, setChatName] = useState<string>("");

    const createChatPress = async () => {
        if (!props.SignalRService) return;
        let newChat = await props.SignalRService.createChat(chatName);
        props.CreateChatCallback(newChat);
        props.CloseCallback();
    };

    return (
        <div className="absolute inset-0 flex items-center justify-center bg-gray-700 bg-opacity-50">
            <div className="flex flex-col w-full p-20 m-8 bg-white rounded-md lg:m-0 sm:p-10 max-w-[500px]">
                <div className="mb-8 text-center">
                    <h1 className="my-3 text-4xl font-bold">Create Chat</h1>
                    <p className="text-sm text-coolGray-600">Create a chat with one or more friends</p>
                </div>
                <form action="" className="">
                    <div className="space-y-4">
                        <div>
                            <label className="block mb-2 text-sm">Name</label>
                            <input
                                value={chatName}
                                onChange={(e: React.FormEvent<HTMLInputElement>) => setChatName(e.currentTarget.value)}
                                name="chatName" placeholder="name"
                                className="w-full px-3 py-2 text-blue-800 border border-blue-300 rounded-md bg-blue-50 focus:outline-none focus:ring-1 focus:ring-blue-300"/>
                        </div>
                    </div>
                    <div className="mt-6">
                        <div className="flex gap-x-2">
                            <button onClick={createChatPress} type="button"
                                    className="w-full px-8 py-3 text-blue-100 bg-blue-600 rounded-md">
                                Create
                            </button>
                            <button onClick={props.CloseCallback} type="button"
                                    className="w-full px-8 py-3 text-blue-600 border border-blue-600 rounded-md">Cancel
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default CreateChatModal;