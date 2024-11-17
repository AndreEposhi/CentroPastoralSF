window.generateKey = async () => {
    const key = await crypto.subtle.generateKey(
        { name: 'AES-GCM', length: 256 },
        true,
        ['encrypt', 'decrypt']
    );
    const keyBytes = await crypto.subtle.exportKey('raw', key);
    return btoa(String.fromCharCode.apply(null, new Uint8Array(keyBytes)));
};

window.generateIV = async () => {
    const iv = crypto.getRandomValues(new Uint8Array(12));
    return btoa(String.fromCharCode.apply(null, iv));
};

async function encrypt(text, keyBase64, ivBase64) {
    const iv = new Uint8Array(atob(ivBase64).split('').map(char => char.charCodeAt(0)));
    const keyBytes = new Uint8Array(atob(keyBase64).split('').map(char => char.charCodeAt(0)));
    const encodedText = new TextEncoder().encode(text);
    const encodedKey = await crypto.subtle.importKey("raw", keyBytes, "AES-GCM", false, ["encrypt"]);
    const encryptedBytes = await crypto.subtle.encrypt({ name: "AES-GCM", iv: iv, tagLength: 128 }, encodedKey, encodedText);
    return btoa(String.fromCharCode.apply(null, new Uint8Array(encryptedBytes)));
}

//async function encrypt(text, keyBase64, ivBase64) {
//    const iv = new Uint8Array(atob(ivBase64).split('').map(char => char.charCodeAt(0)));
//    const keyBytes = new Uint8Array(atob(keyBase64).split('').map(char => char.charCodeAt(0)));
//    const encodedText = new TextEncoder().encode(text);
//    const encodedKey = await crypto.subtle.importKey("raw", keyBytes, "AES-GCM", false, ["encrypt"]);
//    const encryptedBytes = await crypto.subtle.encrypt({ name: "AES-GCM", iv }, encodedKey, encodedText);
//    return btoa(String.fromCharCode.apply(null, new Uint8Array(encryptedBytes)));
//}


//async function encrypt(text, keyBase64, ivBase64) {
//    const iv = new Uint8Array(atob(ivBase64).split('').map(char => char.charCodeAt(0)));
//    const keyBytes = new Uint8Array(atob(keyBase64).split('').map(char => char.charCodeAt(0)));
//    const encodedText = new TextEncoder().encode(text);

//    // Add PKCS#7 padding
//    const pkcs7PaddingLength = 16 - (encodedText.length % 16);
//    const paddedInputBytes = new Uint8Array(encodedText.length + pkcs7PaddingLength);
//    paddedInputBytes.set(encodedText);
//    for (let i = encodedText.length; i < paddedInputBytes.length; i++) {
//        paddedInputBytes[i] = pkcs7PaddingLength;
//    }

//    const encodedKey = await crypto.subtle.importKey("raw", keyBytes, "AES-GCM", false, ["encrypt"]);
//    const encryptedBytes = await crypto.subtle.encrypt({ name: "AES-GCM", iv }, encodedKey, paddedInputBytes);
//    return btoa(String.fromCharCode.apply(null, new Uint8Array(encryptedBytes)));
//}


async function decrypt(encryptedBase64, keyBase64, ivBase64) {
    const iv = new Uint8Array(atob(ivBase64).split('').map(char => char.charCodeAt(0)));
    const keyBytes = new Uint8Array(atob(keyBase64).split('').map(char => char.charCodeAt(0)));
    const encryptedBytes = new Uint8Array(atob(encryptedBase64).split('').map(char => char.charCodeAt(0)));
    const importedKey = await crypto.subtle.importKey("raw", keyBytes, "AES-GCM", false, ["decrypt"]);
    const decryptedBytes = await crypto.subtle.decrypt({ name: "AES-GCM", iv }, importedKey, encryptedBytes);
    return new TextDecoder().decode(decryptedBytes);
}
