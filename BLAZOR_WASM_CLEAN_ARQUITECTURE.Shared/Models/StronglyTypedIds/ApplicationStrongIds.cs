using BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Helpers.StronglyTypedIds;

namespace BLAZOR_WASM_CLEAN_ARQUITECTURE.Shared.Models.StronglyTypedIds;

public record UserId(int Value) : StrongId<UserId>(Value);
