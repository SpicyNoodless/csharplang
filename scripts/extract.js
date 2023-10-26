import fse from "fs-extra";
import { v4 as uuidv4 } from "uuid";
import axios from "axios";

const { readJson, writeJson } = fse;
const key = "";
const endpoint = "";
const location = "";

const base_file_path = "../Assets/English.json";
const target_folder_path = "../Assets";

const result_fr = [];
const result_es = [];
const result_pt = [];

const translate = async (item, target_language) => {
    const response = await axios({
        baseURL: endpoint,
        url: "/translate",
        method: "post",
        headers: {
            "Ocp-Apim-Subscription-Key": key,
            // location required if you're using a multi-service or regional (not global) resource.
            "Ocp-Apim-Subscription-Region": location,
            "Content-type": "application/json",
            "X-ClientTraceId": uuidv4().toString(),
        },
        params: {
            "api-version": "3.0",
            from: "en",
            to: target_language,
        },
        data: [
            {
                text: item.text,
            },
        ],
        responseType: "json",
    });

    const result = response.data[0].translations.map((t) => {
        return {
            text: t.text,
            domain: item.domain,
            intent: item.intent,
            to: t.to,
        };
    });
    return result;
};

const filter_to_language = async (translations, language) => {
    const result = translations
        .flat()
        .filter((t) => t.to === language)
        .map((t) => {
            return {
                text: t.text,
                domain: t.domain,
                intent: t.intent,
            };
        });

    if (language === "fr") {
        result_fr.push(...result);
    }

    if (language === "es") {
        result_es.push(...result);
    }

    if (language === "pt") {
        result_pt.push(...result);
    }
};

const translate_all = async (target_language) => {
    const test_items = (await readJson(base_file_path));
    console.log(`Translating ${test_items.length} items`)
    const translated = await Promise.all(
        test_items.map((t) => translate(t, target_language))
    );
    
    for (const language of target_language) {
        filter_to_language(translated, language);
    }

    await writeJson(`${target_folder_path}/French.json`, result_fr, {
        spaces: 2,
    });
    await writeJson(`${target_folder_path}/Spanish.json`, result_es, {
        spaces: 2,
    });
    await writeJson(`${target_folder_path}/Portuguese.json`, result_pt, {
        spaces: 2,
    });

    console.log("Done");
};

await translate_all(["fr", "pt", "es"]);
