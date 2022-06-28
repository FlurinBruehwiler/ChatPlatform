function IsLoggedIn(): boolean {
  let authCookie = getCookie("X-Access-Token");

  return authCookie != null;
}

function DeleteAuthCookie() {}

function getCookie(name: string) {
  let end;
  const dc = document.cookie;
  const prefix = name + "=";
  let begin = dc.indexOf("; " + prefix);
  if (begin === -1) {
    begin = dc.indexOf(prefix);
    if (begin !== 0) return null;
  } else {
    begin += 2;
    end = document.cookie.indexOf(";", begin);
    if (end === -1) {
      end = dc.length;
    }
  }

  let result = decodeURI(dc.substring(begin + prefix.length, end));
  return result;
}

export default IsLoggedIn;
