using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ffDevelopmentSpace
{


    public class SoundManager
    {

        private static SoundManager instance;

        private Dictionary<string, AudioClip> soundList = new Dictionary<string, AudioClip>();
        private AudioSource audioSource;

        public static SoundManager GetInstance()
        {
            if (null == instance)
            {
                instance = new SoundManager();
            }
            return instance;
        }

        public void SetAudioSource(AudioSource source)
        {
            audioSource = source;
        }

        public void PlayMusic(string name)
        {
            AudioClip ac = null;
            if (soundList.TryGetValue(name, out ac))
            {
                //			audioSource.PlayOneShot(ac);
            }
            else
            {
                string path = PathUtil.getSceneSoundPath(name);//默认是场景音乐
                ac = SingletonMB<ResourceManagerController>.GetInstance().LoadAudioClip(path, name);
                soundList.Add(name, ac);
            }
            audioSource.Stop();
            audioSource.clip = ac;
            audioSource.Play();
        }

        public void PlaySound(string name)
        {
            //AudioClip ac = null;
            //if (soundList.TryGetValue(name, out ac))
            //{

            //}
            //else
            //{
            //    string path = PathUtil.getUiSoundPath("sound");//默认是sound音效资源包
            //    ac = SingletonMB<ResourceManagerController>.GetInstance().LoadAudioClip(path, name);
            //    soundList.Add(name, ac);
            //}
            //audioSource.PlayOneShot(ac);
        }

    }
}
