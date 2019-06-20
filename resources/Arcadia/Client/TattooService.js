API.onEntityStreamIn.connect(function(ent, entType) {
	if (entType === 6 && API.hasEntitySyncedData(ent, "TattooData"))
	{
		API.callNative("CLEAR_PED_DECORATIONS", ent);

		var data = API.getEntitySyncedData(ent, "TattooData");
		data = JSON.parse(data);

		for (var i = 0; i < data.length; i++) API.callNative("_SET_PED_DECORATION", ent, API.getHashKey(data[i].Collection), ((API.getEntityModel(ent) == 1885233650) ? data[i].TattooMaleHash : data[i].TattooFemaleHash));
	}
});