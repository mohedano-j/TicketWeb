//This function allow you to resolved a promise that was not handle from the API.
//This will happen in our system wit
export async function ResolveError(error: any) {
  if (isResponse(error)) {
    var json = await error.json();
    return json;
  }
  return null;
}

function isResponse(value: any) {
  return Boolean(value && typeof value.json === "function");
}

function isPromise(value: any) {
  return Boolean(value && typeof value.then === "function");
}
